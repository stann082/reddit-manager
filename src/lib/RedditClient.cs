using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace lib;

public sealed class RedditClient(RedditTokenProvider tokens, string userAgent)
{

    #region Public Methods

    public async Task<string> GetJsonAsync(string pathOrUrl)
    {
        var url = pathOrUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase)
            ? pathOrUrl
            : $"https://oauth.reddit.com/{pathOrUrl.TrimStart('/')}";

        async Task<HttpResponseMessage> SendAsync(string accessToken)
        {
            var http = new HttpClient();
            http.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", userAgent);
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await http.GetAsync(url);
        }

        var token = await tokens.GetValidAccessTokenAsync();
        var res = await SendAsync(token);

        if (res.StatusCode == HttpStatusCode.Unauthorized)
        {
            // One refresh + retry
            await tokens.ForceRefreshAsync();
            token = await tokens.GetValidAccessTokenAsync();
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

    #endregion
    
}
