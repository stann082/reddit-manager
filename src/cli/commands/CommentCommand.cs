using System.Net;
using System.Text;
using cli.options;
using lib;
using Newtonsoft.Json;
using Reddit;
using Reddit.Inputs.Users;
using Reddit.Things;
using StackExchange.Redis;

namespace cli.commands;

public static class CommentCommand
{

    #region Public Methods

    public static async Task<int> Execute(CommentOptions opts, ApplicationConfig config, IConnectionMultiplexer redis)
    {
        string username = Environment.GetEnvironmentVariable("MY_REDDIT_USERNAME");
        var r = new RedditClient(config.AppId, config.RefreshToken, accessToken: config.AccessToken);
        if (opts.CacheSavedComments)
        {
            await CacheSavedComments(r, redis, username);
        }

        var cachedComments = GetSavedComments(redis);
        var filteredComments = FilterComments(cachedComments, opts);
        var comments = filteredComments.Take(opts.Limit).ToArray();
        foreach (var comment in comments)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Author:      {comment.Author}");
            sb.AppendLine($"Subreddit:   {comment.Subreddit}");
            sb.AppendLine($"Date posted: {comment.CreatedUTC}");
            sb.AppendLine($"Link:        https://reddit.com{comment.Permalink}");
            sb.AppendLine(comment.Body);
            Console.WriteLine(sb.ToString());
        }

        Console.WriteLine($"Total comments: {comments.Length}");
        return await Task.FromResult(0);
    }

    private static Task CacheSavedComments(RedditClient r, IConnectionMultiplexer redis, string username)
    {
        IDatabase db = redis.GetDatabase();
        Console.WriteLine("Caching saved comments into memory");
        var after = "";
        int totalTopComments;
        do
        {
            CommentContainer history = r.Models.Users.CommentHistory(username,
                "saved",
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

        return Task.CompletedTask;
    }

    private static IEnumerable<Comment> FilterComments(IEnumerable<Comment> comments, CommentOptions opts)
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
        else
        {
            Console.WriteLine("Author isn't provided");
        }

        if (filters.TryGetValue("sub", out string subreddit))
        {
            filteredComments = filteredComments.Where(c => c.Subreddit == subreddit);
        }
        else
        {
            Console.WriteLine("Sub isn't provided");
        }

        return filteredComments;
    }

    private static IEnumerable<Comment> GetSavedComments(IConnectionMultiplexer redis)
    {
        IDatabase db = redis.GetDatabase();
        EndPoint endPoint = redis.GetEndPoints().First();
        var keys = redis.GetServer(endPoint).Keys(pattern: "*").ToArray();
        List<Comment> comments = new List<Comment>();

        foreach (var key in keys)
        {
            var cachedValue = db.StringGet(key);
            Comment comment = JsonConvert.DeserializeObject<Comment>(cachedValue);
            if (comment == null)
            {
                Console.WriteLine($"Unable to deserialize a value from {key} key");
                continue;
            }

            comments.Add(comment);
        }

        return comments.OrderByDescending(c => c.CreatedUTC);
    }

    #endregion

}
