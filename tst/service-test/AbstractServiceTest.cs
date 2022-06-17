using Domain;
using Service;

namespace ServiceTest
{
    public abstract class AbstractServiceTest
    {

        #region Constructors

        public AbstractServiceTest()
        {
            ServiceFactoryProxy.Singleton = new ServiceFactory();
        }

        #endregion

    }
}
