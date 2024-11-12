using Newtonsoft.Json;
using Reddit;
using Reddit.Exceptions;
using Reddit.Inputs.Users;
using Reddit.Things;

namespace lib;

public class SearchService(ApplicationConfig config) : ISearchService
{
    #region Public Methods

    public async Task<CommentPreview[]> Search(IOptions options)
    {
        CommentPreview[] commentPreviews;

        if (options.IsArchive)
        {
            commentPreviews = (await GetCommentsFromPushshiftArchive(options))
                .Select(m => new CommentPreview(m))
                .OrderByDescending(m => m.Date).ToArray();
        }
        else
        {
            
            try
            {
                commentPreviews = (await GetCommentsFromReddit(options.User))
                    .Select(c => new CommentPreview(c))
                    .OrderByDescending(c => c.Date).ToArray();
            }
            catch (RedditForbiddenException e)
            {
                Console.WriteLine(e);
                return [];
            }
        }

        string id = options.GetFilterValue("id");
        if (!string.IsNullOrEmpty(id)) return commentPreviews.Where(c => c.Id == id).ToArray();

        IEnumerable<CommentPreview> filteredComments = commentPreviews;
        if (!string.IsNullOrEmpty(options.Query))
            filteredComments = filteredComments.Where(c => c.Body.Contains(options.Query, StringComparison.OrdinalIgnoreCase));

        if (string.IsNullOrEmpty(options.Filter)) return filteredComments.ToArray();

        var author = options.GetFilterValue("author");
        if (!string.IsNullOrEmpty(author))
            filteredComments = filteredComments.Where(c => c.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

        var sub = options.GetFilterValue("sub");
        if (!string.IsNullOrEmpty(sub))
            filteredComments = filteredComments.Where(c => c.Subreddit.Contains(sub, StringComparison.OrdinalIgnoreCase));

        return filteredComments.ToArray();
    }

    #endregion

    #region Variables

    private readonly RedditClient _redditClient = new(config.AppId, config.RefreshToken, accessToken: config.AccessToken);
    private readonly string _me = Environment.GetEnvironmentVariable("MY_REDDIT_USERNAME");

    #endregion

    #region Helper Methods

    private static async Task<CommentModel[]> GetCommentsFromPushshiftArchive(IOptions options)
    {
        List<string> files = [];

        string subreddit = options.GetFilterValue("sub");
        string subredditFolderPattern = !string.IsNullOrEmpty(subreddit) ? subreddit : "*";
        var dirs = Directory.GetDirectories(@"E:\PushshiftDumps\user_comments\author", subredditFolderPattern, SearchOption.AllDirectories);
        foreach (string dir in dirs)
        {
            string[] allFiles = Directory.GetFiles(dir, "*.json", SearchOption.AllDirectories);
            if (allFiles.Length == 0) continue;
            files.AddRange(allFiles);
        }

        string author = options.GetFilterValue("author");
        if (!string.IsNullOrEmpty(author))
        {
            files = files.Where(f => f.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        List<CommentModel> comments = [];
        comments.AddRange(from file in files
            from comment in StreamCommentsFromFile(file)
            select new CommentModel(comment));

        return await Task.FromResult(comments.ToArray());
    }

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

    private static IEnumerable<PushshiftModel> StreamCommentsFromFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        while (reader.ReadLine() is { } line)
        {
            yield return JsonConvert.DeserializeObject<PushshiftModel>(line);
        }
    }

    #endregion
}