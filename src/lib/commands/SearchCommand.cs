using System;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace lib.commands;

/// <summary>
/// Searches Reddit for comments using the Reddit API.
/// This is part of the custom Reddit library implementation that replaced third-party dependencies.
/// 
/// Pagination:
/// The Reddit Search API uses cursor-based pagination via the "after" parameter (NextAfter token).
/// The API does not provide a total count of results. Currently, this command fetches one page
/// of results per invocation. To implement multi-page support, extend this to:
/// 1. Store the NextAfter token between requests
/// 2. Add support for page navigation (next/previous)
/// 3. Implement result caching or cursor history
/// </summary>
public class SearchCommand(IOptions options, ISearchService service) : AbstractCommand(options)
{
    #region Overriden Methods

    protected override Task<CommentModel[]> GetAllComments()
    {
        // does not apply to search
        return Task.FromResult(Array.Empty<CommentModel>());
    }

    protected override async Task<(CommentPreview[] Comments, int total)> GetFilteredComments(IOptions options)
    {
        Log.Debug("Fetching comments from Reddit API with {@Options}", options);
        
        // If ThreadId is provided, fetch all comments from that specific thread
        if (!string.IsNullOrWhiteSpace(options.ThreadId))
        {
            Log.Debug("Fetching comments from thread {ThreadId}", options.ThreadId);
            var threadComments = await service.GetThreadCommentsAsync(options.ThreadId);
            var previews = threadComments.Select(c => new CommentPreview(c)).ToArray();
            return (previews, previews.Length);
        }
        
        // Otherwise, use search API for comment search
        // Reddit API uses cursor-based pagination (NextAfter token) and doesn't provide a total count.
        // We return the comments length as the total since we're fetching a single page of results.
        var (comments, nextAfter) = await service.SearchCommentsAsync(options);
        return (comments, comments.Length);
    }

    #endregion
}