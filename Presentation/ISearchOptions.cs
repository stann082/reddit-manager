namespace Presentation
{
    public interface ISearchOptions
    {

        bool IsPeriodSearchEnabled { get; }

        string Query { get; }

        string ScoreGreaterThan { get; }
        string ScoreLessThan { get; }
        string SortDirection { get; }
        string SortType { get; }
        long StartDateUnixTimeStamp { get; }
        long StopDateUnixTimeStamp { get; }
        string Subreddit { get; }

        string TotalResults { get; }

        string UserName { get; }

    }
}
