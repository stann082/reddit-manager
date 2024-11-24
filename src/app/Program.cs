using lib;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Serilog;

namespace app;

public static class Program
{
    #region Main Method

    public static async Task<int> Main(string[] args)
    {
        Log.Logger = LoggingManager.Initialize();
        try
        {
            var config = ApplicationConfig.Load();
            var services = new ServiceCollection()
                .AddSingleton(config)
                .AddSingleton<App>()
                .AddSingleton(_ => new MongoClient("mongodb://localhost:27017").GetDatabase("reddit"))
                .AddScoped<ICacheService, CacheService>()
                .AddScoped<ISearchService, SearchService>()
                .AddScoped<ISavedService, SavedService>()
                .BuildServiceProvider();
            return await services.GetService<App>().RunApp(args);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
            return 0;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    #endregion
}