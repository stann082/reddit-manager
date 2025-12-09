using System;
using System.Linq;
using System.Threading.Tasks;
using lib.options;
using MongoDB.Driver;
using Reddit;
using Reddit.Inputs.Users;
using Serilog;

namespace lib;

public class CacheService(ApplicationConfig config, IMongoDatabase database, ISavedService savedService) : ICacheService
{
    
    #region Variables

    private readonly RedditClient _redditClient = new(config.AppId, config.RefreshToken, accessToken: config.AccessToken);
    private readonly string _me = Environment.GetEnvironmentVariable("MY_REDDIT_USERNAME");

    #endregion
    
    #region Public Methods

    public async Task CacheSavedCommentsAsync(CacheOptions options)
    {
        var newCachedComments = 0;
        var existingCachedComments = 0;
        Log.Information("Caching saved comments into memory");

        var collection = database.GetCollection<CommentModel>("comments");

        if (options.IsArchive)
        {
            CommentModel[] comments = await savedService.GetCommentsFromPushshiftArchive(options);
            if (comments.Length == 0)
            {
                Log.Information("No comments found in archive");
                return;
            }

            foreach (var comment in comments)
            {
                comment.IsArchive = true;
            }

            var ops = comments.Select(m =>
                new ReplaceOneModel<CommentModel>(Builders<CommentModel>.Filter.Eq(x => x.CommentId, m.CommentId), m)
                {
                    IsUpsert = true
                });

            var result = await collection.BulkWriteAsync(ops);
            Log.Information("Inserted: {UpsertsCount}", result.Upserts.Count);
            Log.Information("Modified: {ModifiedCount}", result.ModifiedCount);
            return;
        }

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
                var filter = Builders<CommentModel>.Filter.Eq("CommentId", topComment.CommentId);
                var document = collection.Find(filter).FirstOrDefault();
                if (document != null)
                {
                    existingCachedComments++;
                    continue;
                }

                await collection.InsertOneAsync(topComment);
                newCachedComments++;
            }

            after = topComments.Last().Name;
            totalTopComments = topComments.Length;
        } while (totalTopComments > 0);

        Log.Information(
            "Cached {NewCachedComments} new comments. Skipped {ExistingCachedComments} comments that were already cached",
            newCachedComments,
            existingCachedComments);
    }

    #endregion

    #region Helper Methods

    private CommentModel[] GetComments(string after)
    {
        try
        {
            var history = _redditClient.Models.Users.CommentHistory(_me, "saved",
                new UsersHistoryInput("comments", after: after, sort: "top", context: 10, limit: 100));
            var comments = history.Data.Children.Select(c => c.Data);
            return comments.Select(c => new CommentModel(c)).ToArray();
        }
        catch (Exception ex)
        {
            LoggingManager.LogException(ex);
            return [];
        }
    }

    #endregion

}