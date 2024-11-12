using System.Text.RegularExpressions;
using MongoDB.Driver;

namespace lib;

public class SavedService(IMongoDatabase database) : ISavedService
{

    #region Public Methods

    public async Task<CommentModel[]> GetAllItemsAsync()
    {
        IEnumerable<CommentModel> comments = database.GetCollection<CommentModel>("comments").AsQueryable().ToArray();
        return await Task.FromResult(comments.ToArray());
    }
    
    public async Task<CommentPreview[]> GetFilteredItemsAsync(IOptions options)
    {
        CommentModel[] comments = await GetAllItemsAsync();
        CommentPreview[] commentPreviews = comments.Select(c => new CommentPreview(c)).ToArray();
        CommentPreview[] allComments = commentPreviews.OrderByDescending(c => c.Date).ToArray();
        return FilterComments(allComments, options).Take(options.Limit).ToArray();
    }

    #endregion

    #region Helper Methods

    private static IEnumerable<CommentPreview> FilterComments(IEnumerable<CommentPreview> comments, IOptions options)
    {
        IEnumerable<CommentPreview> filteredComments = comments;
        if (!string.IsNullOrEmpty(options.Query))
        {
            if (options.IsExactWord)
            {
                string pattern = @"\b" + Regex.Escape(options.Query) + @"\b";
                filteredComments = filteredComments.Where(c => Regex.IsMatch(c.Body, pattern, RegexOptions.IgnoreCase));
            }
            else
            {
                filteredComments = filteredComments.Where(c => c.Body.Contains(options.Query, StringComparison.OrdinalIgnoreCase));
            }
        }

        if (!options.IsFilterEnabled)
        {
            return filteredComments;
        }

        string author = options.Author;
        if (!string.IsNullOrEmpty(author))
        {
            filteredComments = filteredComments.Where(c => c.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
        }

        string sub = options.Subreddit;
        if (!string.IsNullOrEmpty(sub))
        {
            filteredComments = filteredComments.Where(c => c.Subreddit.Equals(sub, StringComparison.OrdinalIgnoreCase));
        }

        return filteredComments;
    }

    #endregion

}
