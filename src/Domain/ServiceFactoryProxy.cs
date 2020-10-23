namespace Domain
{
    public sealed class ServiceFactoryProxy
    {

        public static IServiceFactory Singleton = new NullServiceFactory();

        #region Constructors

        private ServiceFactoryProxy()
        {
        }

        #endregion

    }
}
