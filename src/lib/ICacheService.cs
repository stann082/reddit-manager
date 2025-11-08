using System.Threading.Tasks;
using lib.options;

namespace lib;

public interface ICacheService
{
    Task CacheSavedCommentsAsync(CacheOptions options);
}
