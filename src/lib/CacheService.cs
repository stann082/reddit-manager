using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using lib.options;
using MongoDB.Driver;
using Serilog;

namespace lib;

public class CacheService(IRedditClient redditClient, IMongoDatabase database, ISavedService savedService) : ICacheService
{

    #region Constants/Static Fields

    private static readonly JsonSerializerOptions JsonOpts = new() { PropertyNameCaseInsensitive = true };
    private const int PageLimit = 100;

    #endregion

    #region Public Methods

    public async Task CacheSavedCommentsAsync(CacheOptions options)
    {
        string loggedInUser = await redditClient.LogIn();
        if (string.IsNullOrWhiteSpace(loggedInUser))
        {
            Log.Error("Unable to log in to Reddit");
            return;
        }
        
        var newCachedComments = 0;
        var existingCachedComments = 0;

        Log.Information("Caching saved comments into MongoDB");
        IMongoCollection<CommentModel> collection = database.GetCollection<CommentModel>("comments");

        if (options.IsArchive)
        {
            await CacheArchivedComments(options, collection);
            return;
        }

        string after = null;
        while (true)
        {
            var page = await GetSavedCommentsPageAsync(loggedInUser, after);
            if (page.Comments.Length == 0)
            {
                break;
            }

            var (inserted, modified, matched) = await BulkUpsertByCommentIdAsync(collection, page.Comments);
            newCachedComments += inserted;
            existingCachedComments += Math.Max(0, matched - modified);

            after = page.NextAfter;
            if (string.IsNullOrWhiteSpace(after))
            {
                break;
            }
        }

        Log.Information(
            "Cached {NewCachedComments} new comments. Skipped {ExistingCachedComments} comments that were already cached",
            newCachedComments,
            existingCachedComments);
    }

    #endregion

    #region Helper Methods

    private static async Task<(int Inserted, int Modified, int Matched)> BulkUpsertByCommentIdAsync(
        IMongoCollection<CommentModel> collection,
        IReadOnlyList<CommentModel> models)
    {
        if (models.Count == 0)
        {
            return (0, 0, 0);
        }

        var ops = models.Select(m =>
                new ReplaceOneModel<CommentModel>(
                        Builders<CommentModel>.Filter.Eq(x => x.CommentId, m.CommentId),
                        m)
                    { IsUpsert = true })
            .ToList();

        var result = await collection.BulkWriteAsync(ops, new BulkWriteOptions { IsOrdered = false });
        var inserted = result.Upserts.Count;
        var modified = (int)result.ModifiedCount;
        var matched = (int)result.MatchedCount;
        return (inserted, modified, matched);
    }

    private async Task CacheArchivedComments(CacheOptions options, IMongoCollection<CommentModel> collection)
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
    }

    private async Task<(CommentModel[] Comments, string NextAfter)> GetSavedCommentsPageAsync(string username, string after)
    {
        try
        {
            var qs = new List<string>
            {
                $"limit={PageLimit}",
                "sort=top",
                "type=comments",
                "context=10"
            };
            if (!string.IsNullOrWhiteSpace(after))
            {
                qs.Add($"after={Uri.EscapeDataString(after)}");
            }

            var url = $"https://oauth.reddit.com/user/{Uri.EscapeDataString(username)}/saved?{string.Join("&", qs)}";
            var json = await redditClient.GetJsonAsync(url);
            var listing = JsonSerializer.Deserialize<RedditListing<RedditThing<JsonElement>>>(json, JsonOpts);
            var children = listing?.Data?.Children;
            if (children is null || children.Count == 0)
            {
                return ([], null);
            }

            var results = new List<CommentModel>(capacity: children.Count);
            results.AddRange(from child in children
                where string.Equals(child.Kind, "t1", StringComparison.OrdinalIgnoreCase)
                select child.Data.Deserialize<RedditComment>(JsonOpts)
                into comment
                where comment is not null
                select new CommentModel(comment));

            // IMPORTANT: use listing cursor, not Last().Name
            return (results.ToArray(), listing.Data?.After);
        }
        catch (Exception ex)
        {
            LoggingManager.LogException(ex);
            return ([], null);
        }
    }

    #endregion
    
}