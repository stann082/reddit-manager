using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace lib;

public sealed class RedditClient : IRedditClient
{

    #region Constants
    
    private const string UserAgent = "windows:mekkeron_saved_cache:1.0 (by /u/mekkeron)";
    
    #endregion

    #region Variables

    private readonly RedditTokenProvider _tokens = new(ApplicationConfig.AppConfigFilePath, UserAgent);

    #endregion
    
    #region Public Methods

    public async Task<string> GetJsonAsync(string pathOrUrl)
    {
        string url = pathOrUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase)
            ? pathOrUrl
            : $"https://oauth.reddit.com/{pathOrUrl.TrimStart('/')}";

        async Task<HttpResponseMessage> SendAsync(string accessToken)
        {
            var http = new HttpClient();
            http.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", UserAgent);
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await http.GetAsync(url);
        }

        var token = await _tokens.GetValidAccessTokenAsync();
        var res = await SendAsync(token);

        if (res.StatusCode == HttpStatusCode.Unauthorized)
        {
            // One refresh + retry
            await _tokens.ForceRefreshAsync();
            token = await _tokens.GetValidAccessTokenAsync();
            res = await SendAsync(token);
        }

        var body = await res.Content.ReadAsStringAsync();

        // Catch “network policy” HTML blocks early so you don’t JSON-parse garbage
        if (res.StatusCode == HttpStatusCode.Forbidden && body.Contains("blocked due to a network policy", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("Reddit blocked this request due to a network policy (IP/VPN/User-Agent).");
        }

        if (!res.IsSuccessStatusCode)
        {
            throw new Exception($"GET {url} failed: {(int)res.StatusCode} {res.StatusCode}\n{body}");
        }

        return body;
    }

    public async Task<string> LogIn()
    {
        return await GetLoggedInUsername();
    }

    #endregion
    
    #region Helper Methods
    
    private async Task<string> GetLoggedInUsername()
    {
        string me = await GetJsonAsync("api/v1/me");
        using JsonDocument meDoc = JsonDocument.Parse(me);
        return meDoc.RootElement.GetProperty("name").GetString();
    }
    
    #endregion

}
