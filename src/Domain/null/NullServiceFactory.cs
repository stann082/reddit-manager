namespace Domain
{
    public class NullServiceFactory : IServiceFactory
    {

        #region Public Methods

        public IRedditApiService CreateRedditService()
        {
            return NullRedditApiService.Singleton;
        }

        #endregion

    }
}
