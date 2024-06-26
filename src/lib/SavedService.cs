using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Reddit.Things;
using StackExchange.Redis;

namespace lib;

public class SavedService : ISavedService
{

    #region Constructors

    public SavedService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    #endregion

    #region Variables

    private readonly IConnectionMultiplexer _redis;

    #endregion

    #region Public Methods

    public async Task<CommentPreview[]> GetFilteredItemsAsync(IOptions savedOptions)
    {
        IDatabase db = _redis.GetDatabase();
        EndPoint endPoint = _redis.GetEndPoints().First();
        var keys = _redis.GetServer(endPoint).Keys(pattern: "*").ToArray();
        List<Comment> comments = new List<Comment>();

        foreach (var key in keys)
        {
            try
            {
                RedisValue cachedValue = await db.StringGetAsync(key);
                Comment comment = JsonConvert.DeserializeObject<Comment>(cachedValue);
                if (comment == null)
                {
                    Console.WriteLine($"Unable to deserialize a value from {key} key");
                    continue;
                }

                comments.Add(comment);
            }
            catch (Exception)
            {
                // squash
            }
        }

        CommentPreview[] commentPreviews = comments.Select(c => new CommentPreview(c)).ToArray();
        CommentPreview[] allComments = commentPreviews.OrderByDescending(c => c.Date).ToArray();
        return FilterComments(allComments, savedOptions).ToArray();
    }

    #endregion

    #region Helper Methods

    private static IEnumerable<CommentPreview> FilterComments(IEnumerable<CommentPreview> comments, IOptions options)
    {
        IEnumerable<CommentPreview> filteredComments = comments;
        if (!string.IsNullOrEmpty(options.Query))
        {
            if (options.IsExactWord)
            {
                string pattern = @"\b" + Regex.Escape(options.Query) + @"\b";
                filteredComments = filteredComments.Where(c => Regex.IsMatch(c.Body, pattern, RegexOptions.IgnoreCase));
            }
            else
            {
                filteredComments = filteredComments.Where(c => c.Body.Contains(options.Query, StringComparison.OrdinalIgnoreCase));
            }
        }

        if (string.IsNullOrEmpty(options.Filter))
        {
            return filteredComments;
        }

        string author = options.GetFilterValue("author");
        if (!string.IsNullOrEmpty(author))
        {
            filteredComments = filteredComments.Where(c => c.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
        }

        string sub = options.GetFilterValue("sub");
        if (!string.IsNullOrEmpty(sub))
        {
            filteredComments = filteredComments.Where(c => c.Subreddit.Equals(sub, StringComparison.OrdinalIgnoreCase));
        }

        return filteredComments;
    }

    #endregion

}
