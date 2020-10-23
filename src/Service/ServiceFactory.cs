using Domain;

namespace Service
{
    public class ServiceFactory : IServiceFactory
    {

        #region Public Methods

        public IRedditApiService CreateRedditService()
        {
            return new RedditApiService();
        }

        #endregion

    }
}
