﻿using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Reddit;
using Reddit.Inputs.Users;
using Serilog;

namespace lib;

public class CacheService(ApplicationConfig config, IMongoDatabase database) : ICacheService
{
    
    #region Variables

    private readonly RedditClient _redditClient = new(config.AppId, config.RefreshToken, accessToken: config.AccessToken);
    private readonly string _me = Environment.GetEnvironmentVariable("MY_REDDIT_USERNAME");

    #endregion
    
    #region Public Methods

    public async Task CacheSavedCommentsAsync()
    {
        var newCachedComments = 0;
        var existingCachedComments = 0;
        Log.Information("Caching saved comments into memory");

        var collection = database.GetCollection<CommentModel>("comments");

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