using cli.commands;
using cli.options;
using CommandLine;

namespace app;

public class App
{
    
    #region Constructors

    public App(/*ApplicationConfig config, ILoginService loginService, ISpotifyService spotifyService*/)
    {
        // _config = config;
        // _loginService = loginService;
        // _spotifyService = spotifyService;
    }

    #endregion

    #region Variables

    // private readonly ApplicationConfig _config;
    // private readonly ILoginService _loginService;
    // private readonly ISpotifyService _spotifyService;

    #endregion

    #region Public Methods

    public async Task<int> RunApp(IEnumerable<string> args)
    {
        return await Parser.Default.ParseArguments<AuthTokenOptions /*,
                FavoritesOptions,
                LoginOptions,
                LogoutOptions,
                PlaylistsOptions,
                RestoreOptions,
                TracksOptions*/>(args)
            .MapResult(
                async (AuthTokenOptions opts) => await AuthTokenCommand.Execute(opts),
                // async (FavoritesOptions opts) => await FavoritesCommand.Execute(opts, _spotifyService),
                // async (LoginOptions opts) => await LoginCommand.Execute(opts, _config, _loginService),
                // async (LogoutOptions _) => await LogoutCommand.Execute(_config),
                // async (PlaylistsOptions opts) => await PlaylistsCommand.Execute(opts, _spotifyService),
                // async (RestoreOptions opts) => await RestoreCommand.Execute(opts, _spotifyService),
                // async (TracksOptions opts) => await TracksCommand.Execute(opts, _spotifyService),
                errs => Task.FromResult(1));
        return 0;
    }

    #endregion

}
