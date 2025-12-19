using System.Text.Json;
using System.Threading.Tasks;
using Serilog;

namespace lib.commands;

public static class AuthenticationCommand
{

    #region Public Methods

    public static async Task<int> Execute()
    {
        const string ua = "windows:mekkeron_saved_cache:1.0 (by /u/mekkeron)";
        RedditTokenProvider tokens = new RedditTokenProvider(ApplicationConfig.AppConfigFilePath, ua);
        RedditClient reddit = new RedditClient(tokens, ua);
        string me = await reddit.GetJsonAsync("api/v1/me");
        using JsonDocument meDoc = JsonDocument.Parse(me);
        var user = meDoc.RootElement.GetProperty("name").GetString();
        Log.Information("{Me} is logged in", user);

        // TODO: Use this to fetch comments from saved or search
        // var saved = await reddit.GetJsonAsync($"user/{user}/saved?limit=100");
        // Console.WriteLine(saved.Substring(0, Math.Min(500, saved.Length)));
        return 0;
    }

    #endregion

}