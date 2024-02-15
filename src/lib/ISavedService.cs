using Reddit.Things;

namespace lib;

public interface ISavedService
{
    Task CacheSavedCommentsAsync();
    Task<CommentPreview[]> GetFilteredItemsAsync(IOptions savedOptions);
}
