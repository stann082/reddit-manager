namespace lib;

public interface ISavedService
{
    Task<CommentModel[]> GetAllItemsAsync();
    Task<CommentPreview[]> GetFilteredItemsAsync(IOptions options);
    Task<long> GetTotalCommentsCount();
}
