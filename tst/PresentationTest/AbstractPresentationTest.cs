using Domain;

namespace PresentationTest
{
    public abstract class AbstractPresentationTest
    {

        #region Constructors

        public AbstractPresentationTest()
        {
            ServiceFactoryProxy.Singleton = new MockServiceFactory();
        }

        #endregion

    }
}
