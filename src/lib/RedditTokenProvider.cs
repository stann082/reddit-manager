using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace lib;

public sealed class RedditTokenProvider(string configPath, string userAgent)
{

    #region Public Methods

    public async Task ForceRefreshAsync()
    {
        var cfg = LoadConfig();
        var (token, expiresIn) = await RefreshAsync(cfg.app_id, cfg.refresh_token);
        cfg.access_token = token;
        cfg.expires_at_utc = DateTime.UtcNow.AddSeconds(expiresIn);
        SaveConfig(cfg);
    }

    public async Task<string> GetValidAccessTokenAsync()
    {
        var cfg = LoadConfig();
        if (IsAccessTokenFresh(cfg) && !string.IsNullOrWhiteSpace(cfg.access_token))
        {
            return cfg.access_token;
        }

        var (token, expiresIn) = await RefreshAsync(cfg.app_id, cfg.refresh_token);
        cfg.access_token = token;
        cfg.expires_at_utc = DateTime.UtcNow.AddSeconds(expiresIn);
        SaveConfig(cfg);

        return token;
    }

    #endregion

    #region Helper Methods

    private static bool IsAccessTokenFresh(RedditConfig cfg)
    {
        // Refresh a bit early to avoid edge cases
        if (cfg.expires_at_utc is null) return false;
        return DateTime.UtcNow < cfg.expires_at_utc.Value.AddMinutes(-2);
    }

    private RedditConfig LoadConfig()
    {
        if (!File.Exists(configPath))
        {
            var appId = Environment.GetEnvironmentVariable("REDDIT_API_CLIENT_ID");
            var refreshToken = Environment.GetEnvironmentVariable("REDDIT_API_REFRESH_TOKEN");
            if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new InvalidOperationException("Could not find REDDIT_API_CLIENT_ID or REDDIT_API_REFRESH_TOKEN in environment variables.");
            }
            
            return new RedditConfig { app_id = appId, refresh_token = refreshToken };
        }
        
        var json = File.ReadAllText(configPath);
        var cfg = JsonSerializer.Deserialize<RedditConfig>(json);
        if (string.IsNullOrWhiteSpace(cfg.app_id) || string.IsNullOrWhiteSpace(cfg.refresh_token))
        {
            throw new InvalidOperationException("config.json must include app_id and refresh_token.");
        }
        
        return cfg;
    }

    private void SaveConfig(RedditConfig cfg)
    {
        var json = JsonSerializer.Serialize(cfg, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(configPath, json);
    }

    private async Task<(string AccessToken, int ExpiresIn)> RefreshAsync(string clientId, string refreshToken)
    {
        using var http = new HttpClient();
        http.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", userAgent);

        // Installed-app style: empty secret
        var basic = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:"));
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basic);

        var form = new FormUrlEncodedContent([
            new KeyValuePair<string,string>("grant_type", "refresh_token"),
            new KeyValuePair<string,string>("refresh_token", refreshToken)
        ]);

        var res = await http.PostAsync("https://www.reddit.com/api/v1/access_token", form);
        var body = await res.Content.ReadAsStringAsync();

        if (!res.IsSuccessStatusCode)
            throw new Exception($"Refresh failed: {(int)res.StatusCode} {res.StatusCode}\n{body}");

        using var doc = JsonDocument.Parse(body);
        var token = doc.RootElement.GetProperty("access_token").GetString();
        var expires = doc.RootElement.TryGetProperty("expires_in", out var e) ? e.GetInt32() : 3600;

        if (string.IsNullOrWhiteSpace(token))
            throw new Exception("Refresh succeeded but access_token was empty.");

        return (token!, expires);
    }

    #endregion
    
}

