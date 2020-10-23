using Domain;

namespace PresentationTest
{
    public class MockServiceFactory : NullServiceFactory
    {

        #region Constructors

        public MockServiceFactory()
        {
            MockRedditApiService = new MockRedditApiService();
        }

        #endregion

        #region Properties

        private IRedditApiService MockRedditApiService { get; set; }

        #endregion

        #region Public Methods

        public override IRedditApiService CreateRedditService()
        {
            return MockRedditApiService;
        }

        #endregion

    }
}
