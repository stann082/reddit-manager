using Domain;
using System.Threading.Tasks;

namespace PresentationTest
{
    public class MockRedditApiService : NullRedditApiService
    {

        #region Public Methods

        public RedditData Data { get; set; }

        #endregion

        #region Public Methods

        public override Task<RedditData> GetRedditData(string requestUri)
        {
            return Task<RedditData>.Factory.StartNew(() => Data);
        }

        #endregion

    }
}
