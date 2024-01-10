using cli.commands;
using cli.options;
using CommandLine;
using lib;

namespace app;

public class App
{

    #region Constructors

    public App(ISavedService savedService)
    {
        _savedService = savedService;
    }

    #endregion

    #region Variables

    private readonly ISavedService _savedService;

    #endregion

    #region Public Methods

    public async Task<int> RunApp(IEnumerable<string> args)
    {
        return await Parser.Default.ParseArguments<AuthenticationOptions, CommentsOptions>(args)
            .MapResult(
                async (AuthenticationOptions opts) => await AuthenticationCommand.Execute(opts),
                async (CommentsOptions opts) => await CommentsCommand.Execute(opts, _savedService),
                _ => Task.FromResult(1));
    }

    #endregion

}
