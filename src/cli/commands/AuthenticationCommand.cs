using System.Diagnostics;
using System.Runtime.InteropServices;
using cli.options;
using Reddit.AuthTokenRetriever;
using Reddit.AuthTokenRetriever.EventArgs;
using Serilog;

namespace cli.commands;

public static class AuthenticationCommand
{

    #region Constants

    private const string BrowserPath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

    #endregion
    
    #region Public Methods

    public static Task<int> Execute(AuthenticationOptions opts)
    {
        Log.Debug("Starting token retrieval utility with {@Options}....", opts);
        
        Console.WriteLine("Opening browser to Reddit authentication page....");
        AuthTokenRetrieverLib authTokenRetrieverLib = new AuthTokenRetrieverLib(opts.AppId, opts.Port, appSecret: opts.AppSecret);
        authTokenRetrieverLib.AuthSuccess += C_AuthSuccess;
        authTokenRetrieverLib.AwaitCallback(true);
        OpenBrowser(authTokenRetrieverLib.AuthURL());
        
        Console.WriteLine("Awaiting Reddit callback -OR- press any key to abort....");
        Console.ReadKey();
        authTokenRetrieverLib.StopListening();
        Console.WriteLine("Token retrieval utility terminated.");
        return Task.FromResult(0);
    }

    #endregion

    #region Helper Methods

    private static void C_AuthSuccess(object sender, AuthSuccessEventArgs e)
    {
        Console.Clear();
        Console.WriteLine("Token retrieval successful!\n");
        Console.WriteLine($"Access Token: {e.AccessToken}");
        Console.WriteLine($"Refresh Token: {e.RefreshToken}\n");
        Console.WriteLine("Press any key to exit....");
    }

    private static void OpenBrowser(string authUrl = "about:blank")
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(authUrl);
                Process.Start(processStartInfo);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(BrowserPath)
                {
                    Arguments = authUrl
                };
                Process.Start(processStartInfo);
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            // For OSX run a separate command to open the web browser as found in https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
            Process.Start("open", authUrl);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            // Similar to OSX, Linux can (and usually does) use xdg for this task.
            Process.Start("xdg-open", authUrl);
        }
    }

    #endregion
    
}
