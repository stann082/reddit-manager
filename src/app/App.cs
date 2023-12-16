using cli.commands;
using cli.options;
using CommandLine;
using lib;
using StackExchange.Redis;

namespace app;

public class App
{
    
    #region Constructors

    public App(ApplicationConfig config, IConnectionMultiplexer redis)
    {
        _config = config;
        _redis = redis;
    }

    #endregion

    #region Variables

    private readonly ApplicationConfig _config;
    private readonly IConnectionMultiplexer _redis;

    #endregion

    #region Public Methods

    public async Task<int> RunApp(IEnumerable<string> args)
    {
        return await Parser.Default.ParseArguments<AuthenticationOptions, CommentOptions>(args)
            .MapResult(
                async (AuthenticationOptions opts) => await AuthenticationCommand.Execute(opts),
                async (CommentOptions opts) => await CommentCommand.Execute(opts, _config, _redis),
                _ => Task.FromResult(1));
    }

    #endregion

}
