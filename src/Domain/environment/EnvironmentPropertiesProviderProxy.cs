namespace Domain
{
    public sealed class EnvironmentPropertiesProviderProxy
    {

        public static IEnvironmentProperties Singleton = new EnvironmentProperties();

        #region Constructors

        private EnvironmentPropertiesProviderProxy()
        {
        }

        #endregion

    }
}
