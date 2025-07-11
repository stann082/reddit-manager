using System;
using System.IO;
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
        string logFilePath = Path.Combine(baseLogPath, "logs", "reddit", DateTime.Today.ToString("yyyy-MM-dd"), "usage.log");
        
        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File(logFilePath, shared: true, rollingInterval: RollingInterval.Day);

        if (configuration != null)
        {
            loggerConfiguration
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext();
        }

        return loggerConfiguration.CreateLogger();
    }
    
    public static void LogException(Exception ex)
    {
        Log.Fatal(ex, "An unhandled exception occurred");
    }

    #endregion
    
}