using lib;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace app;

public static class Program
{
    
    #region Main Method

    public static async Task<int> Main(string[] args)
    {
        var config = ApplicationConfig.Load();
        var services = new ServiceCollection()
        .AddSingleton(config)
        .AddSingleton<App>()
        .AddSingleton<IConnectionMultiplexer>(await ConnectionMultiplexer.ConnectAsync("localhost"))
        .AddScoped<ISearchService, SearchService>()
        .AddScoped<ISavedService, SavedService>()
        .BuildServiceProvider();
        return await services.GetService<App>().RunApp(args);
    }

    #endregion

}
