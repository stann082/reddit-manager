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
            HttpClient.BaseAddress = new Uri(Constants.BASE_URL);
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        #region Properties

        private HttpClient HttpClient { get; set; }

        #endregion

        #region Public Methods

        public async Task<RedditData> GetRedditData(string requestUri)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string responseJson = await response.Content.ReadAsStringAsync();
            RedditData data = JsonConvert.DeserializeObject<RedditData>(responseJson);
            return data;
        }

        #endregion

    }
}
