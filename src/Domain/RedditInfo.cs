using Newtonsoft.Json;

namespace Domain
{
    public class RedditData
    {

        [JsonProperty("data")]
        public RedditInfo[] Contents { get; set; }

    }
}
