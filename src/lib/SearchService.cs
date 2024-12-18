using Reddit;
using Reddit.Exceptions;
using Reddit.Inputs.Users;
using Reddit.Things;

namespace lib;

public class SearchService(ApplicationConfig config) : AbstractService, ISearchService
{
    
    #region Variables

    private readonly RedditClient _redditClient = new(config.AppId, config.RefreshToken, accessToken: config.AccessToken);
    private readonly string _me = Environment.GetEnvironmentVariable("MY_REDDIT_USERNAME");

    #endregion
    
    #region Public Methods

    public async Task<(CommentPreview[], int)> Search(IOptions options)
    {
        CommentPreview[] comments;

        try
        {
            comments = (await GetCommentsFromReddit(options.Author))
                .Select(c => new CommentPreview(c))
                .OrderByDescending(c => c.Date).ToArray();
        }
        catch (RedditForbiddenException ex)
        {
            LoggingManager.LogException(ex);
            return ([], 0);
        }

        var filteredComments = FilterComments(comments, options).ToArray();
        var pagedComments = filteredComments.Take(options.Limit).ToArray();
        return (pagedComments, filteredComments.Length);
    }

    #endregion

    #region Helper Methods

    private async Task<CommentModel[]> GetCommentsFromReddit(string username)
    {
        List<Comment> comments = [];

        var after = "";
        int totalComments;
        do
        {
            var commentsBatch = await Task.Run(() =>
            {
                var history = _redditClient.Models.Users.CommentHistory(
                    !string.IsNullOrEmpty(username) ? username : _me, "comments",
                    new UsersHistoryInput("comments", after: after, context: 10, limit: 100));
                return history.Data.Children.Select(c => c.Data).ToArray();
            });

            if (commentsBatch.Length == 0)
            {
                totalComments = 0;
                continue;
            }

            comments.AddRange(commentsBatch);
            after = commentsBatch.Last().Name;
            totalComments = commentsBatch.Length;
        } while (totalComments > 0);

        return comments.Select(c => new CommentModel(c)).ToArray();
    }

    #endregion

}