using System.Text;

namespace Domain
{
    public class UrlParameterBuilder
    {

        #region Constructors

        public UrlParameterBuilder(ISearchOptions options)
        {
            Options = options;
        }

        #endregion

        #region Properties

        private ISearchOptions Options { get; set; }

        #endregion

        #region Public Methods

        public string Build()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('?');

            if (!string.IsNullOrEmpty(Options.Query))
            {
                string formattedText = Options.Query.Replace(' ', '+');
                sb.Append($"q={formattedText}");
            }

            if (!string.IsNullOrEmpty(Options.UserName))
            {
                sb.Append($"&author={Options.UserName}");
            }

            if (!string.IsNullOrEmpty(Options.Subreddit))
            {
                sb.Append($"&subreddit={Options.Subreddit}");
            }

            if (!string.IsNullOrEmpty(Options.TotalResults))
            {
                sb.Append($"&size={Options.TotalResults}");
            }

            if (Options.IsPeriodSearchEnabled)
            {
                sb.Append($"&after={Options.StartDateUnixTimeStamp}&before={Options.StopDateUnixTimeStamp}");
            }

            if (!string.IsNullOrEmpty(Options.ScoreGreaterThan))
            {
                sb.Append($"&score=>{Options.ScoreGreaterThan}");
            }

            if (!string.IsNullOrEmpty(Options.ScoreLessThan))
            {
                sb.Append($"&score=<{Options.ScoreLessThan}");
            }

            sb.Append($"&sort_type={Options.SortType}");
            sb.Append($"&sort={Options.SortDirection}");

            return sb.ToString();
        }

        #endregion
    }
}
