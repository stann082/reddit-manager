using cli.commands;
using cli.options;
using CommandLine;
using lib;

namespace app;

public class App(ICacheService cacheService, ISavedService savedService, ISearchService searchService)
{

    #region Public Methods

    public async Task<int> RunApp(IEnumerable<string> args)
    {
        return await Parser.Default.ParseArguments<AuthenticationOptions, SavedOptions, SearchOptions, CacheOptions>(args)
            .MapResult(
                async (AuthenticationOptions opts) => await AuthenticationCommand.Execute(opts),
                async (SearchOptions opts) => await new SearchCommand(opts, searchService).Execute(),
                async (SavedOptions opts) => await new SavedCommand(opts, savedService).Execute(),
                async (CacheOptions _) => await new CacheCommand(cacheService).Execute(),
                _ => Task.FromResult(1));
    }

    #endregion

}
