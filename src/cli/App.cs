using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;
using lib;
using lib.commands;
using lib.options;

namespace cli;

public class App(
    ICacheService cacheService,
    ISavedService savedService,
    ISearchService searchService)
{
    #region Public Methods

    public async Task<int> RunApp(IEnumerable<string> args)
    {
        return await Parser.Default
            .ParseArguments<AuthenticationOptions, SavedOptions, SearchOptions, CacheOptions>(args)
            .MapResult(
                async (AuthenticationOptions opts) => await AuthenticationCommand.Execute(opts),
                async (SearchOptions opts) => await new SearchCommand(opts, searchService).Execute(),
                async (SavedOptions opts) => await new SavedCommand(opts, savedService).Execute(),
                async (CacheOptions opts) => await new CacheCommand(opts, cacheService).Execute(),
                _ => Task.FromResult(1));
    }

    #endregion
}
