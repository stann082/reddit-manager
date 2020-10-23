using Domain;

namespace TestEnvironment
{
    public class MockSearchOptions : ISearchOptions
    {

        #region Properties

        public bool IsPeriodSearchEnabled { get; set; }

        public string Query { get; set; }

        public string ScoreGreaterThan { get; set; }
        public string ScoreLessThan { get; set; }
        public bool ShowExactMatches { get; set; }
        public string SortDirection { get; set; }
        public string SortType { get; set; }
        public long StartDateUnixTimeStamp { get; set; }
        public long StopDateUnixTimeStamp { get; set; }
        public string Subreddit { get; set; }

        public string TotalResults { get; set; }

        public string UserName { get; set; }

        #endregion

    }
}
