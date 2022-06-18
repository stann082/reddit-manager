using Newtonsoft.Json;

namespace Domain
{
    public class Submission : AbstractContent, IContent
    {

        #region Properties

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("created_utc")]
        public long CreatedUtc { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("selftext")]
        public string Message { get; set; }

        [JsonProperty("no_follow")]
        public bool NoFollow { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("quote")]
        public string Quote => GetQuote(Message);

        [JsonProperty("retrieved_on")]
        public long RetrievedOn { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("send_replies")]
        public bool SendReplies { get; set; }

        [JsonProperty("stickied")]
        public bool Stickied { get; set; }

        [JsonProperty("subreddit")]
        public string Subreddit { get; set; }

        [JsonProperty("subreddit_id")]
        public string SubredditId { get; set; }

        [JsonIgnore]
        public long? UpdatedUtc => null;

        #endregion

    }
}
