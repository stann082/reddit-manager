using Microsoft.Extensions.DependencyInjection;

namespace app;

public static class Program
{
    
    #region Main Method

    public static async Task<int> Main(string[] args)
    {
        // var config = ApplicationConfig.Load();
        var services = new ServiceCollection()
        .AddSingleton<App>()
        //     .AddSingleton(config)
        //     .AddSingleton<ISpotifyService, SpotifyService>()
        // .AddSingleton<ILoginService, LoginService>()
        .BuildServiceProvider();
        return await services.GetService<App>().RunApp(args);
    }

    #endregion

}
