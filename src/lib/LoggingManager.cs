using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace lib;

public static class LoggingManager
{

    #region Public Methods

    public static Logger Initialize(IConfigurationRoot configuration = null)
    {
        string baseLogPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string logFilePath = Path.Combine(baseLogPath, "logs", "reddit", DateTime.Today.ToString("d"), "usage.log");
        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File(logFilePath, shared: true, rollingInterval: RollingInterval.Day);
        
        if (configuration == null)
        {
            return loggerConfiguration.CreateLogger();
        }
        
        return loggerConfiguration
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
    }
    
    public static void LogException(Exception ex)
    {
        Log.Fatal(ex, "An unhandled exception occurred");
    }

    #endregion
    
}