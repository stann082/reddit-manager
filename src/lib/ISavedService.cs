using Reddit.Things;

namespace lib;

public interface ISavedService
{
    Task<CommentPreview[]> GetFilteredItemsAsync(IOptions savedOptions);
}
