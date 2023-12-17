using cli.commands;
using cli.options;
using CommandLine;
using lib;

namespace app;

public class App
{

    #region Constructors

    public App(IRedditService redditService)
    {
        _redditService = redditService;
    }

    #endregion

    #region Variables

    private readonly IRedditService _redditService;

    #endregion

    #region Public Methods

    public async Task<int> RunApp(IEnumerable<string> args)
    {
        return await Parser.Default.ParseArguments<AuthenticationOptions, CommentOptions>(args)
            .MapResult(
                async (AuthenticationOptions opts) => await AuthenticationCommand.Execute(opts),
                async (CommentOptions opts) => await CommentCommand.Execute(opts, _redditService),
                _ => Task.FromResult(1));
    }

    #endregion

}
