using System.Net;
using Newtonsoft.Json;
using Reddit;
using Reddit.Inputs.Users;
using Reddit.Things;
using StackExchange.Redis;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace lib;

public class SavedService : ISavedService
{

    #region Constructors

    public SavedService(ApplicationConfig config, IConnectionMultiplexer redis)
    {
        _me = Environment.GetEnvironmentVariable("MY_REDDIT_USERNAME");
        _redditClient = new RedditClient(config.AppId, config.RefreshToken, accessToken: config.AccessToken);
        _redis = redis;
    }

    #endregion

    #region Variables

    private readonly RedditClient _redditClient;
    private readonly IConnectionMultiplexer _redis;
    private readonly string _me;

    #endregion

    #region Public Methods

    public async Task CacheSavedCommentsAsync()
    {
        int newCachedComments = 0;
        int existingCachedComments = 0;
        IDatabase db = _redis.GetDatabase();
        Console.WriteLine("Caching saved comments into memory");

        var after = "";
        int totalTopComments;
        do
        {
            var topComments = await Task.Run(() => GetComments(after));
            if (!topComments.Any())
            {
                totalTopComments = 0;
                continue;
            }

            foreach (var topComment in topComments)
            {
                string key = topComment.Id;
                string cachedValue = db.StringGet(key);
                if (!string.IsNullOrEmpty(cachedValue))
                {
                    existingCachedComments++;
                    continue;
                }

                string value = JsonSerializer.Serialize(topComment);
                db.StringSet(key, value);
                newCachedComments++;
            }

            after = topComments.Last().Name;
            totalTopComments = topComments.Length;
        } while (totalTopComments > 0);

        Console.WriteLine($"Cached {newCachedComments} new comments. Skipped {existingCachedComments} comments that were already cached.");
    }

    public async Task<CommentPreview[]> GetFilteredItemsAsync(IOptions savedOptions)
    {
        if (savedOptions.GetCached)
        {
            return await GetCachedCommentsAsync(savedOptions);
        }

        // TODO:SNB - Abstract posts and comments under a single interface
        CommentPreview[] comments = savedOptions.Comment ? await GetSavedComments() : await GetSavedPosts();
        CommentPreview[] allComments = comments.OrderByDescending(c => c.CreatedUTC).ToArray();
        IEnumerable<CommentPreview> filteredComments = FilterComments(allComments, savedOptions);
        return filteredComments.ToArray();
    }

    #endregion

    #region Helper Methods

    private static IEnumerable<CommentPreview> FilterComments(IEnumerable<CommentPreview> comments, IOptions options)
    {
        IEnumerable<CommentPreview> filteredComments = comments;
        if (!string.IsNullOrEmpty(options.Query))
        {
            filteredComments = filteredComments.Where(c => c.Body.Contains(options.Query, StringComparison.OrdinalIgnoreCase));
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
            filteredComments = filteredComments.Where(c => c.Subreddit.Contains(sub, StringComparison.OrdinalIgnoreCase));
        }

        return filteredComments;
    }

    private async Task<CommentPreview[]> GetSavedComments()
    {
        IDatabase db = _redis.GetDatabase();

        List<Comment> comments = new List<Comment>();

        var after = "";
        int totalTopComments;
        do
        {
            var topComments = await Task.Run(() =>
            {
                CommentContainer history = _redditClient.Models.Users.CommentHistory(_me, "saved",
                    new UsersHistoryInput("comments", after: after, sort: "top", context: 10, limit: 100));
                return history.Data.Children.Select(c => c.Data).ToArray();
            });
            if (!topComments.Any())
            {
                totalTopComments = 0;
                continue;
            }

            comments.AddRange(topComments);
            after = topComments.Last().Name;
            totalTopComments = topComments.Length;
        } while (totalTopComments > 0);

        return comments.Select(c => new CommentPreview(c)).ToArray();
    }

    private static async Task<CommentPreview[]> GetSavedPosts()
    {
        return await Task.FromResult(Array.Empty<CommentPreview>());
    }

    private async Task<CommentPreview[]> GetCachedCommentsAsync(IOptions options)
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
        CommentPreview[] allComments = commentPreviews.OrderByDescending(c => c.CreatedUTC).ToArray();
        return FilterComments(allComments, options).ToArray();
    }

    private Comment[] GetComments(string after)
    {
        CommentContainer history = _redditClient.Models.Users.CommentHistory(_me, "saved",
            new UsersHistoryInput("comments", after: after, sort: "top", context: 10, limit: 100));
        return history.Data.Children.Select(c => c.Data).ToArray();
    }

    #endregion

}
