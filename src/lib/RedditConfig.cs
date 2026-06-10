using System;

namespace lib;

public sealed class RedditConfig
{
    public string app_id { get; set; } = "";
    public string refresh_token { get; set; } = "";
    public string access_token { get; set; } = "";
    public DateTime? expires_at_utc { get; set; }
}
