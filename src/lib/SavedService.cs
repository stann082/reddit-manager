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
    
    public async Task<CommentPreview[]> GetFilteredItemsAsync(IOptions options)
    {
        CommentModel[] comments = await GetAllItemsAsync();
        CommentPreview[] commentPreviews = comments.Select(c => new CommentPreview(c)).ToArray();
        CommentPreview[] allComments = commentPreviews.OrderByDescending(c => c.Date).ToArray();
        return FilterComments(allComments, options).Take(options.Limit).ToArray();
    }

    public async Task<long> GetTotalCommentsCount()
    {
        var collection = database.GetCollection<CommentModel>("comments");
        return await collection.CountDocumentsAsync(FilterDefinition<CommentModel>.Empty);
    }

    #endregion

}
