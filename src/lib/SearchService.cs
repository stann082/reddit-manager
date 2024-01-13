using Reddit;
using Reddit.Controllers;
using Reddit.Inputs.Search;

namespace lib;

public class SearchService : ISearchService
{

    #region Constructors

    public SearchService(ApplicationConfig config)
    {
        _redditClient = new RedditClient(config.AppId, config.RefreshToken, accessToken: config.AccessToken);
    }

    #endregion

    #region Variables

    private readonly RedditClient _redditClient;

    #endregion

    #region Public Methods

    public async Task<Post[]> Search(ISearchOptions options)
    {
        var filterString = options.Filter;
        var filters = filterString.Split('&')
            .Select(part => part.Split('='))
            .Where(parts => parts.Length == 2)
            .ToDictionary(parts => parts[0], parts => parts[1]);

        string subreddit = filters.GetValueOrDefault("sub", "all");
        var posts = await Task.Run(() => _redditClient.Subreddit(subreddit).Search(new SearchGetSearchInput(options.Query)));
        return posts.ToArray();
    }

    #endregion

    #region Helper Methods

    private static async Task<Comment[]> GetPosts()
    {
        return await Task.FromResult(Array.Empty<Comment>());
    }

    #endregion

}
