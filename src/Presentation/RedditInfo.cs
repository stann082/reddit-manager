using Newtonsoft.Json;

namespace Presentation
{
    public class RedditData
    {

        [JsonProperty("data")]
        public RedditInfo[] Contents { get; set; }

    }
}
