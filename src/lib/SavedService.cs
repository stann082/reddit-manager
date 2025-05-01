using MongoDB.Driver;
using Newtonsoft.Json;

namespace lib;

public class SavedService(IMongoDatabase database) : AbstractService, ISavedService
{

    #region Public Methods

    public async Task<CommentModel[]> GetAllItemsAsync()
    {
        var comments = database.GetCollection<CommentModel>("comments").AsQueryable().ToArray();
        return await Task.FromResult(comments.ToArray());
    }
    
    public async Task<(CommentPreview[], int)> GetFilteredItemsAsync(IOptions options)
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
            commentPreviews = (await GetAllItemsAsync()).Select(c => new CommentPreview(c)).ToArray();
        }
        
        CommentPreview[] allComments = commentPreviews.OrderByDescending(c => c.Date).ToArray();
        CommentPreview[] filteredComments = FilterComments(allComments, options).ToArray();
        CommentPreview[] pagedComments = filteredComments.Take(options.Limit).ToArray();
        return (pagedComments, filteredComments.Length);
    }

    #endregion

    #region Helper Methods

    private static async Task<CommentModel[]> GetCommentsFromPushshiftArchive(IOptions options)
    {
        List<string> files = [];

        string subreddit = options.Subreddit;
        string subredditFolderPattern = !string.IsNullOrEmpty(subreddit) ? subreddit : "*";
        var basePath = Environment.GetEnvironmentVariable("PUSHSHIFT_PATH") ?? @"E:\PushshiftDumps\user_comments\author";
        var dirs = Directory.GetDirectories(basePath, subredditFolderPattern, SearchOption.AllDirectories);
        foreach (string dir in dirs)
        {
            string[] allFiles = Directory.GetFiles(dir, "*.json", SearchOption.AllDirectories);
            if (allFiles.Length == 0) continue;
            files.AddRange(allFiles);
        }

        string author = options.Author;
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
