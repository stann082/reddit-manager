using lib;

namespace cli.commands;

public class CacheCommand(ICacheService service)
{

    #region Public Methods

    public async Task<int> Execute()
    {
        await service.CacheSavedCommentsAsync();
        return await Task.FromResult(0);
    }

    #endregion

}
