using lib;
using Serilog;

namespace cli.commands;

public class CacheCommand(ICacheService service)
{

    #region Public Methods

    public async Task<int> Execute()
    {
        Log.Information("Caching saved comments with Reddit API");
        await service.CacheSavedCommentsAsync();
        return await Task.FromResult(0);
    }

    #endregion

}
