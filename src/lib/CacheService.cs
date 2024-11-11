using Reddit;
using Reddit.Inputs.Users;
using Reddit.Things;
using StackExchange.Redis;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace lib;

public class CacheService(ApplicationConfig config, IConnectionMultiplexer redis) : ICacheService
{

    #region Variables

    private readonly RedditClient _redditClient = new(config.AppId, config.RefreshToken, accessToken: config.AccessToken);
    private readonly string _me = Environment.GetEnvironmentVariable("MY_REDDIT_USERNAME");

    #endregion

    #region Public Methods

    public async Task CacheSavedCommentsAsync()
    {
        int newCachedComments = 0;
        int existingCachedComments = 0;
        IDatabase db = redis.GetDatabase();
        Console.WriteLine("Caching saved comments into memory");

        var after = "";
        int totalTopComments;
        do
        {
            var topComments = await Task.Run(() => GetComments(after));
            if (topComments.Length == 0)
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

    #endregion

    #region Helper Methods

    private Comment[] GetComments(string after)
    {
        CommentContainer history = _redditClient.Models.Users.CommentHistory(_me, "saved",
            new UsersHistoryInput("comments", after: after, sort: "top", context: 10, limit: 100));
        return history.Data.Children.Select(c => c.Data).ToArray();
    }

    #endregion

}
