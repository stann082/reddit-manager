using Domain;
using Service;

namespace Presentation
{
    public class PresentationEnvironment
    {

        public static PresentationEnvironment Singleton = new PresentationEnvironment();

        #region Constructors

        private PresentationEnvironment()
        {
        }

        #endregion

        #region Public Methods

        public void InitializeServices()
        {
            ServiceFactoryProxy.Singleton = new ServiceFactory();
        }

        #endregion

    }
}
