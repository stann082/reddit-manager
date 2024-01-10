using Reddit;
using Reddit.Inputs.Users;
using Reddit.Things;

namespace lib;

public class SavedService : ISavedService
{

    #region Constructors

    public SavedService(ApplicationConfig config)
    {
        _username = Environment.GetEnvironmentVariable("MY_REDDIT_USERNAME");
        _redditClient = new RedditClient(config.AppId, config.RefreshToken, accessToken: config.AccessToken);
    }

    #endregion

    #region Variables

    private readonly RedditClient _redditClient;
    private readonly string _username;

    #endregion

    #region Public Methods

    public async Task<Task> CacheSavedCommentsAsync()
    {
        Console.WriteLine("Feature temporarily disabled");
        return await Task.FromResult(Task.CompletedTask);
    }

    public async Task<Comment[]> GetFilteredCommentsAsync(IOptions options)
    {
        // TODO:SNB - Abstract posts and comments under a single interface
        Comment[] comments = options.Comment ? await GetSavedComments() : await GetSavedPosts();
        var allComments = comments.OrderByDescending(c => c.CreatedUTC).ToArray();
        IEnumerable<Comment> filteredComments = FilterComments(allComments, options);
        return filteredComments.ToArray();
    }

    #endregion

    #region Helper Methods

    private static IEnumerable<Comment> FilterComments(IEnumerable<Comment> comments, IOptions opts)
    {
        IEnumerable<Comment> filteredComments = comments;
        if (!string.IsNullOrEmpty(opts.Query))
        {
            filteredComments = filteredComments.Where(c => c.Body.Contains(opts.Query, StringComparison.OrdinalIgnoreCase));
        }

        if (string.IsNullOrEmpty(opts.Filter))
        {
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
            filteredComments = filteredComments.Where(c => c.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
        }

        if (filters.TryGetValue("sub", out string subreddit))
        {
            filteredComments = filteredComments.Where(c => c.Subreddit.Contains(subreddit, StringComparison.OrdinalIgnoreCase));
        }

        return filteredComments;
    }

    private async Task<Comment[]> GetSavedComments()
    {
        List<Comment> comments = new List<Comment>();

        var after = "";
        int totalTopComments;
        do
        {
            var topComments = await Task.Run(() =>
            {
                CommentContainer history = _redditClient.Models.Users.CommentHistory(_username, "saved",
                    new UsersHistoryInput("comments", after: after, sort: "top", context: 10, limit: 100));
                return history.Data.Children.Select(c => c.Data).ToArray();
            });
            if (!topComments.Any())
            {
                totalTopComments = 0;
                continue;
            }

            comments.AddRange(topComments);
            after = topComments.Last().Name;
            totalTopComments = topComments.Length;
        } while (totalTopComments > 0);

        return comments.ToArray();
    }

    private static async Task<Comment[]> GetSavedPosts()
    {
        return await Task.FromResult(Array.Empty<Comment>());
    }

    #endregion

}
