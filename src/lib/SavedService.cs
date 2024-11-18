using MongoDB.Driver;

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
        CommentModel[] comments = await GetAllItemsAsync();
        CommentPreview[] commentPreviews = comments.Select(c => new CommentPreview(c)).ToArray();
        CommentPreview[] allComments = commentPreviews.OrderByDescending(c => c.Date).ToArray();
        CommentPreview[] filteredComments = FilterComments(allComments, options).ToArray();
        CommentPreview[] pagedComments = filteredComments.Take(options.Limit).ToArray();
        return (pagedComments, filteredComments.Length);
    }

    #endregion

}
