using Newtonsoft.Json;

namespace Domain
{
    public class SearchRequestModel : ISearchOptions
    {

        #region Properties

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("subreddit")]
        public string Subreddit { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        #endregion

        #region ISearchOptions Members

        [JsonIgnore]
        public bool IsPeriodSearchEnabled => false;

        [JsonIgnore]
        public QueryType QueryType => QueryType.Comment;

        [JsonIgnore]
        public string ScoreGreaterThan => string.Empty;

        [JsonIgnore]
        public string ScoreLessThan => string.Empty;

        [JsonIgnore]
        public bool ShowExactMatches => false;

        [JsonIgnore]
        public string SortDirection => string.Empty;

        [JsonIgnore]
        public string SortType => string.Empty;

        [JsonIgnore]
        public long StartDateUnixTimeStamp => 0;

        [JsonIgnore]
        public long StopDateUnixTimeStamp => 0;

        [JsonIgnore]
        public string TotalResults => Size;

        [JsonIgnore]
        public string UserName => Author;

        #endregion

    }
}
