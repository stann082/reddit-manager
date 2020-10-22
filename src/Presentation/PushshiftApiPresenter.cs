using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class PushshiftApiPresenter
    {

        #region Constants

        private static readonly DateTime START_DATE = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private const string BASE_URL = "https://api.pushshift.io/reddit/search/comment";

        #endregion

        #region Constructors

        public PushshiftApiPresenter()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(BASE_URL);
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        #region Properties

        public string Counter { get; set; }
        public string Response { get; set; }

        private HttpClient HttpClient { get; set; }

        #endregion

        #region Public Methods

        public async Task BuildResponseContent(ISearchOptions options)
        {
            string requestUri = BuildUrlParameters(options);
            HttpResponseMessage response = await HttpClient.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                Response = "Could not connect to Reddit servers...";
                return;
            }

            string responseJson = await response.Content.ReadAsStringAsync();
            RedditData data = JsonConvert.DeserializeObject<RedditData>(responseJson);

            RedditInfo[] contents = data.Contents;
            if (options.ShowExactMatches)
            {
                contents = data.Contents.Where(c => c.Body.Contains(options.Query)).ToArray();
            }

            if (contents.Length == 0)
            {
                Response = "Nothing found...";
                return;
            }

            Counter = $"Showing {contents.Length} items";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"API call: {response.RequestMessage.RequestUri}");
            sb.AppendLine();

            foreach (RedditInfo content in contents)
            {
                sb.AppendLine($"Subreddit: {content.Subreddit}");
                sb.AppendLine($"    Score: {content.Score}");
                sb.AppendLine($"  Created: {FromUnixTime(content.CreatedUtc)}");

                DateTime updatedTime = FromUnixTime(content.UpdatedUtc);
                if (updatedTime > START_DATE)
                {
                    sb.AppendLine($"  Updated: {updatedTime}");
                }

                if (string.IsNullOrEmpty(options.UserName))
                {
                    sb.AppendLine($"   Author: https://www.reddit.com/u/{content.Author}");
                }

                sb.AppendLine();

                content.Body = content.Body.Replace("&gt;", "*****");
                sb.AppendLine(content.Body);
                sb.AppendLine($"Link: https://www.reddit.com{content.Permalink}");

                sb.AppendLine();
                sb.AppendLine();
            }

            Response = sb.ToString();
        }

        #endregion

        #region Helper Methods

        private string BuildUrlParameters(ISearchOptions options)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('?');

            if (!string.IsNullOrEmpty(options.Query))
            {
                string formattedText = options.Query.Replace(' ', '+');
                sb.Append($"q={formattedText}");
            }

            if (!string.IsNullOrEmpty(options.UserName))
            {
                sb.Append($"&author={options.UserName}");
            }

            if (!string.IsNullOrEmpty(options.Subreddit))
            {
                sb.Append($"&subreddit={options.Subreddit}");
            }

            if (!string.IsNullOrEmpty(options.TotalResults))
            {
                sb.Append($"&size={options.TotalResults}");
            }

            if (options.IsPeriodSearchEnabled)
            {
                sb.Append($"&after={options.StartDateUnixTimeStamp}&before={options.StopDateUnixTimeStamp}");
            }

            if (!string.IsNullOrEmpty(options.ScoreGreaterThan))
            {
                sb.Append($"&score=>{options.ScoreGreaterThan}");
            }

            if (!string.IsNullOrEmpty(options.ScoreLessThan))
            {
                sb.Append($"&score=<{options.ScoreLessThan}");
            }

            sb.Append($"&sort_type={options.SortType}");
            sb.Append($"&sort={options.SortDirection}");

            return sb.ToString();
        }

        private DateTime FromUnixTime(double unixTimeStamp)
        {
            DateTime dtDateTime = START_DATE;
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        #endregion

    }
}
