using Domain;
using TestEnvironment;

namespace PresentationTest
{
    public abstract class AbstractPresentationTest : AbstractTest
    {

        #region Constructors

        protected AbstractPresentationTest()
        {
            ServiceFactoryProxy.Singleton = new MockServiceFactory();
        }

        #endregion

    }
}
