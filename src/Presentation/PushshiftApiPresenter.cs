using Domain;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class PushshiftApiPresenter
    {

        #region Constants

        private static readonly DateTime START_DATE = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        #endregion

        #region Constructors

        public PushshiftApiPresenter()
        {
            Service = ServiceFactoryProxy.Singleton.CreateRedditService();
        }

        #endregion

        #region Properties

        public string Counter { get; set; }
        public string Response { get; set; }

        private IRedditApiService Service { get; set; }

        #endregion

        #region Public Methods

        public async Task BuildResponseContent(ISearchOptions options)
        {
            UrlParameterBuilder builder = new UrlParameterBuilder(options);
            string requestUri = builder.Build();

            RedditData data = await Service.GetRedditData(requestUri);
            if (data == null)
            {
                Response = "Something went wrong...";
                return;
            }

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

            sb.AppendLine($"API call: {Constants.BASE_URL}{requestUri}");
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

        private DateTime FromUnixTime(double unixTimeStamp)
        {
            DateTime dtDateTime = START_DATE;
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        #endregion

    }
}
