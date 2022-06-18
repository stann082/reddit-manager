using Domain;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Presentation
{
    public class SearchPresenter
    {

        #region Constants

        private static readonly DateTime START_DATE = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        #endregion

        #region Constructors

        public SearchPresenter(IRedditApiService service)
        {
            Service = service;
        }

        #endregion

        #region Properties

        public string Counter { get; private set; }
        public string Response { get; private set; }

        private IRedditApiService Service { get; set; }

        #endregion

        #region Public Methods

        public async Task<IRedditData> GetData(ISearchOptions options)
        {
            UrlParameterBuilder builder = new(options);
            string requestUri = builder.Build(options.QueryType.ToString().ToLower());

            IRedditData data = await GetRedditData(requestUri, options.QueryType);
            if (data == null)
            {
                Log.Error("Something went wrong... Please check the logs");
                return null;
            }

            return data;
        }

        #endregion

        #region Helper Methods

        private async Task<IRedditData> GetRedditData(string requestUri, QueryType queryType)
        {
            switch (queryType)
            {
                case QueryType.Comment:
                    return await Service.GetCommentData(requestUri);
                case QueryType.Submission:
                    return await Service.GetSubmissionData(requestUri);
                default:
                    return null;
            }
        }

        #endregion

    }
}
