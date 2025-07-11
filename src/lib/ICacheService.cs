using System.Threading.Tasks;

namespace lib;

public interface ICacheService
{
    Task CacheSavedCommentsAsync();
}
