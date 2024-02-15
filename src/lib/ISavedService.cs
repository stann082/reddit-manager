using Reddit.Things;

namespace lib;

public interface ISavedService
{
    Task CacheSavedCommentsAsync();
    Task<Comment[]> GetFilteredItemsAsync(IOptions savedOptions);
}
