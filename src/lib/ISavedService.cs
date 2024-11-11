using Reddit.Things;

namespace lib;

public interface ISavedService
{
    Task<Comment[]> GetAllItemsAsync();
    Task<CommentPreview[]> GetFilteredItemsAsync(IOptions savedOptions);
}
