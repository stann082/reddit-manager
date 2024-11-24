using Serilog;
using Serilog.Core;

namespace lib;

public static class LoggingManager
{

    #region Public Methods

    public static Logger Initialize()
    {
        string baseLogPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string logFilePath = Path.Combine(baseLogPath, "logs", "reddit", "{Date}", "usage.log");
        return new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
    
    public static void LogException(Exception ex)
    {
        Log.Fatal(ex, "An unhandled exception occurred");
    }

    #endregion
    
}