﻿using cli.commands;
using cli.options;
using CommandLine;
using lib;

namespace app;

public class App
{

    #region Constructors

    public App(ICacheService cacheService, ISavedService savedService, ISearchService searchService)
    {
        _cacheService = cacheService;
        _savedService = savedService;
        _searchService = searchService;
    }

    #endregion

    #region Variables

    private readonly ICacheService _cacheService;
    private readonly ISearchService _searchService;
    private readonly ISavedService _savedService;

    #endregion

    #region Public Methods

    public async Task<int> RunApp(IEnumerable<string> args)
    {
        return await Parser.Default.ParseArguments<AuthenticationOptions, SavedOptions, SearchOptions, CacheOptions>(args)
            .MapResult(
                async (AuthenticationOptions opts) => await AuthenticationCommand.Execute(opts),
                async (SearchOptions opts) => await new SearchCommand(opts, _searchService).Execute(),
                async (SavedOptions opts) => await new SavedCommand(opts, _savedService).Execute(),
                async (CacheOptions opts) => await new CacheCommand(opts, _cacheService).Execute(),
                _ => Task.FromResult(1));
    }

    #endregion

}
