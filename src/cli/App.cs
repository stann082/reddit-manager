using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;
using lib;
using lib.commands;
using lib.options;
using MongoDB.Driver;

namespace cli;

public class App(
    ICacheService cacheService,
    ISavedService savedService,
    ISearchService searchService,
    IMongoDatabase database)
{
    #region Public Methods

    public async Task<int> RunApp(IEnumerable<string> args)
    {
        return await Parser.Default
            .ParseArguments<AuthenticationOptions, SavedOptions, SearchOptions, CacheOptions, MigrateOptions>(args)
            .MapResult(
                async (AuthenticationOptions opts) => await AuthenticationCommand.Execute(opts),
                async (SearchOptions opts) => await new SearchCommand(opts, searchService).Execute(),
                async (SavedOptions opts) => await new SavedCommand(opts, savedService).Execute(),
                async (CacheOptions _) => await new CacheCommand(cacheService).Execute(),
                async (MigrateOptions _) => await new MigrateCommand(savedService, database).Execute(),
                _ => Task.FromResult(1));
    }

    #endregion
}