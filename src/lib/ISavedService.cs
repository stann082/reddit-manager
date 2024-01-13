using Reddit.Things;

namespace lib;

public interface ISavedService
{
    Task<Comment[]> GetFilteredItemsAsync(ISavedOptions savedOptions);
}
