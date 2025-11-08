using System.Threading.Tasks;
using lib.options;

namespace lib.commands;

public class CacheCommand(CacheOptions options, ICacheService service)
{
    #region Public Methods

    public async Task<int> Execute()
    {
        await service.CacheSavedCommentsAsync(options);
        return await Task.FromResult(0);
    }

    #endregion
}