using Reddit;
using Reddit.Exceptions;
using Reddit.Inputs.Users;
using Reddit.Things;

namespace lib;

public class SearchService : ISearchService
{

    #region Constructors

    public SearchService(ApplicationConfig config)
    {
        _me = Environment.GetEnvironmentVariable("MY_REDDIT_USERNAME");
        _redditClient = new RedditClient(config.AppId, config.RefreshToken, accessToken: config.AccessToken);
    }

    #endregion

    #region Variables

    private readonly RedditClient _redditClient;
    private readonly string _me;

    #endregion

    #region Public Methods

    public async Task<CommentPreview[]> Search(IOptions options)
    {
        Comment[] comments;
        try
        {
            comments = options.Comment ? await GetComments(options.User) : await GetPosts();
        }
        catch (RedditForbiddenException e)
        {
            Console.WriteLine(e);
            return Array.Empty<CommentPreview>();
        }

        var allComments = comments.Select(c => new CommentPreview(c)).OrderByDescending(c => c.Date).ToArray();
        IEnumerable<CommentPreview> filteredComments = FilterComments(allComments, options);
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

    private async Task<Comment[]> GetComments(string username)
    {
        List<Comment> comments = new List<Comment>();

        var after = "";
        int totalComments;
        do
        {
            var commentsBatch = await Task.Run(() =>
            {
                CommentContainer history = _redditClient.Models.Users.CommentHistory(!string.IsNullOrEmpty(username) ? username : _me, "comments",
                    new UsersHistoryInput("comments", after: after, context: 10, limit: 100));
                return history.Data.Children.Select(c => c.Data).ToArray();
            });

            if (!commentsBatch.Any())
            {
                totalComments = 0;
                continue;
            }

            comments.AddRange(commentsBatch);
            after = commentsBatch.Last().Name;
            totalComments = commentsBatch.Length;
        } while (totalComments > 0);

        return comments.ToArray();
    }

    private static async Task<Comment[]> GetPosts()
    {
        return await Task.FromResult(Array.Empty<Comment>());
    }

    #endregion

}
