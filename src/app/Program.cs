using lib;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

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
        .AddSingleton(_ => new MongoClient("mongodb://localhost:27017").GetDatabase("reddit"))
        .AddScoped<ICacheService, CacheService>()
        .AddScoped<ISearchService, SearchService>()
        .AddScoped<ISavedService, SavedService>()
        .BuildServiceProvider();
        return await services.GetService<App>().RunApp(args);
    }

    #endregion

}
