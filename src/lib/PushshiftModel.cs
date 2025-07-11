using System.Collections.Generic;
using Newtonsoft.Json;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace lib;

public class PushshiftModel
{
    
    [JsonProperty("author")]
    public string Author { get; set; }

    [JsonProperty("author_created_utc")]
    public long? AuthorCreatedUtc { get; set; }

    [JsonProperty("author_flair_background_color")]
    public string AuthorFlairBackgroundColor { get; set; }

    [JsonProperty("author_flair_css_class")]
    public string AuthorFlairCssClass { get; set; }

    [JsonProperty("author_flair_richtext")]
    public List<object> AuthorFlairRichtext { get; set; }

    [JsonProperty("author_flair_template_id")]
    public string AuthorFlairTemplateId { get; set; }

    [JsonProperty("author_flair_text")]
    public string AuthorFlairText { get; set; }

    [JsonProperty("author_flair_text_color")]
    public string AuthorFlairTextColor { get; set; }

    [JsonProperty("author_flair_type")]
    public string AuthorFlairType { get; set; }

    [JsonProperty("author_fullname")]
    public string AuthorFullname { get; set; }

    [JsonProperty("author_patreon_flair")]
    public bool AuthorPatreonFlair { get; set; }

    [JsonProperty("body")]
    public string Body { get; set; }

    [JsonProperty("can_gild")]
    public bool CanGild { get; set; }

    [JsonProperty("can_mod_post")]
    public bool CanModPost { get; set; }

    [JsonProperty("collapsed")]
    public bool Collapsed { get; set; }

    [JsonProperty("collapsed_reason")]
    public string CollapsedReason { get; set; }

    [JsonProperty("controversiality")]
    public int Controversiality { get; set; }

    [JsonProperty("created_utc")]
    public long CreatedUtc { get; set; }

    [JsonProperty("distinguished")]
    public string Distinguished { get; set; }

    [JsonProperty("edited")]
    public bool Edited { get; set; }

    [JsonProperty("gilded")]
    public int Gilded { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("is_submitter")]
    public bool IsSubmitter { get; set; }

    [JsonProperty("link_id")]
    public string LinkId { get; set; }

    [JsonProperty("no_follow")]
    public bool NoFollow { get; set; }

    [JsonProperty("parent_id")]
    public string ParentId { get; set; }

    [JsonProperty("permalink")]
    public string Permalink { get; set; }

    [JsonProperty("quarantined")]
    public bool Quarantined { get; set; }

    [JsonProperty("removal_reason")]
    public string RemovalReason { get; set; }

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

    [JsonProperty("subreddit_name_prefixed")]
    public string SubredditNamePrefixed { get; set; }

    [JsonProperty("subreddit_type")]
    public string SubredditType { get; set; }
    
}