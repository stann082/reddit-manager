using System.Threading.Tasks;

namespace Domain
{
    public class NullRedditApiService : IRedditApiService
    {

        public static IRedditApiService Singleton = new NullRedditApiService();

        #region Public Methods

        public Task<RedditData> GetRedditData(string requestUri)
        {
            return null;
        }

        #endregion

    }
}
