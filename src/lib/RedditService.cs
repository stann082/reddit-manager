using System.Net;
using Newtonsoft.Json;
using Reddit;
using Reddit.Inputs.Users;
using Reddit.Things;
using StackExchange.Redis;

namespace lib;

public class RedditService : IRedditService
{

    #region Constructors

    public RedditService(ApplicationConfig config, IConnectionMultiplexer redis)
    {
        _username = Environment.GetEnvironmentVariable("MY_REDDIT_USERNAME");
        _redditClient = new RedditClient(config.AppId, config.RefreshToken, accessToken: config.AccessToken);
        _redis = redis;
    }

    #endregion

    #region Variables

    private readonly RedditClient _redditClient;
    private readonly IConnectionMultiplexer _redis;
    private readonly string _username;

    #endregion
    
    #region Public Methods

    public async Task<Task> CacheSavedCommentsAsync()
    {
        IDatabase db = _redis.GetDatabase();
        Console.WriteLine("Caching saved comments into memory");
        var after = "";
        int totalTopComments;
        do
        {
            CommentContainer history = _redditClient.Models.Users.CommentHistory(_username, "saved",
                new UsersHistoryInput("comments", after: after, sort: "top", context: 10, limit: 100));
            var topComments = history.Data.Children.Select(c => c.Data).ToList();
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
                    continue;
                }

                string value = JsonConvert.SerializeObject(topComment);
                db.StringSet(key, value);
            }

            after = topComments.Last().Name;
            totalTopComments = topComments.Count;
        } while (totalTopComments > 0);

        return await Task.FromResult(Task.CompletedTask);
    }
    
    public async Task<Comment[]> GetFilteredCommentsAsync(ICommentOptions options)
    {
        IDatabase db = _redis.GetDatabase();
        EndPoint endPoint = _redis.GetEndPoints().First();
        var keys = _redis.GetServer(endPoint).Keys(pattern: "*").ToArray();
        List<Comment> comments = new List<Comment>();

        foreach (var key in keys)
        {
            var cachedValue = await db.StringGetAsync(key);
            Comment comment = JsonConvert.DeserializeObject<Comment>(cachedValue);
            if (comment == null)
            {
                Console.WriteLine($"Unable to deserialize a value from {key} key");
                continue;
            }

            comments.Add(comment);
        }

        var allComments = comments.OrderByDescending(c => c.CreatedUTC).ToArray();
        return FilterComments(allComments, options).ToArray();
    }

    #endregion

    #region Helper Methods

    private static IEnumerable<Comment> FilterComments(IEnumerable<Comment> comments, ICommentOptions opts)
    {
        IEnumerable<Comment> filteredComments = comments;
        if (!string.IsNullOrEmpty(opts.Query))
        {
            filteredComments = filteredComments.Where(c => c.Body.Contains(opts.Query, StringComparison.OrdinalIgnoreCase));
        }

        if (string.IsNullOrEmpty(opts.Filter))
        {
            Console.WriteLine("No valid filter options provided");
            return filteredComments;
        }

        var filterString = opts.Filter;
        var filters = filterString.Split('&')
            .Select(part => part.Split('='))
            .Where(parts => parts.Length == 2)
            .ToDictionary(parts => parts[0], parts => parts[1]);
        if (!filters.Any())
        {
            return filteredComments;
        }

        if (filters.TryGetValue("author", out string author))
        {
            filteredComments = filteredComments.Where(c => c.Author == author);
        }

        if (filters.TryGetValue("sub", out string subreddit))
        {
            filteredComments = filteredComments.Where(c => c.Subreddit == subreddit);
        }

        return filteredComments.Take(opts.Limit);
    }

    #endregion

}
