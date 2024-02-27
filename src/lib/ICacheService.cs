using Reddit.Things;

namespace lib;

public interface ICacheService
{
    Task CacheSavedCommentsAsync();
}
