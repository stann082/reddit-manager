using lib.options;

namespace lib.services;

public interface ISavedService
{
    Task<CommentPreview[]> GetFilteredItemsAsync(IOptions savedOptions);
}
