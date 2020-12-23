using Domain;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Service
{
    public class RedditApiService : IRedditApiService
    {

        #region Constructors

        public RedditApiService()
        {
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        #region Properties

        private HttpClient HttpClient { get; set; }

        #endregion

        #region Public Methods

        public async Task<IRedditData> GetCommentData(string requestUri)
        {
            return await GetData<CommentData>(requestUri, QueryType.Comment.ToString().ToLower());
        }

        public async Task<IRedditData> GetSubmissionData(string requestUri)
        {
            return await GetData<SubmissionData>(requestUri, QueryType.Submission.ToString().ToLower());
        }

        #endregion

        #region Helper Methods

        private async Task<T> GetData<T>(string requestUri, string queryType)
        {
            HttpClient.BaseAddress = new Uri($"{Constants.BASE_URL}/{queryType}");

            HttpResponseMessage response = await HttpClient.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            string responseJson = await response.Content.ReadAsStringAsync();

            T data = default;
            try
            {
                data = JsonConvert.DeserializeObject<T>(responseJson);
            }
            catch (Exception)
            {
                // TODO: log exception
            }

            return data;
        }

        #endregion

    }
}
