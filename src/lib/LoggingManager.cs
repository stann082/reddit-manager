using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace lib;

public static class LoggingManager
{

    #region Public Methods

    public static Logger Initialize()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build();
        
        string baseLogPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string logFilePath = Path.Combine(baseLogPath, "logs", "reddit", DateTime.Today.ToString("d"), "usage.log");
        return new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(logFilePath, shared: true, rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
    
    public static void LogException(Exception ex)
    {
        Log.Fatal(ex, "An unhandled exception occurred");
    }

    #endregion
    
}