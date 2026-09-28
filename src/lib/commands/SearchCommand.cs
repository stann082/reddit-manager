using System;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace lib.commands;

/// <summary>
/// Searches Reddit for comments using the Reddit API.
/// This is part of the custom Reddit library implementation that replaced third-party dependencies.
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
        
        // Otherwise, search the author's (default: logged-in user's) comment history
        return await service.SearchCommentsAsync(options);
    }

    #endregion
}