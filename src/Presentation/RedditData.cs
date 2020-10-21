using Newtonsoft.Json;

namespace Presentation
{
    public class RedditInfo
    {

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("author_fullname")]
        public string AuthorFullName { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("created_utc")]
        public long CreatedUtc { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("is_submitter")]
        public bool IsSubmitter { get; set; }

        [JsonProperty("link_id")]
        public string LinkId { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("no_follow")]
        public bool NoFollow { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

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

        [JsonProperty("total_awards_received")]
        public int TotalAwardsReceived { get; set; }

        [JsonProperty("updated_utc")]
        public long UpdatedUtc { get; set; }

    }

}
