using Newtonsoft.Json;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace lib;

public class PushshiftModel
{
    public string Author { get; set; }
    
    public string Body { get; set; }
    
    [JsonProperty("created_utc")]
    public long CreatedUtc { get; set; }
    
    public string Id { get; set; }
    
    public string Permalink { get; set; }
    
    public int Score { get; set; }
    public string Subreddit { get; set; }
}