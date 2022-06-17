using Domain;
using System.IO;

namespace TestEnvironment
{
    public class MockEnvironmentProperties : IEnvironmentProperties
    {

        #region Properties

        public string ProgId { get; set; }

        #endregion

        #region Public Methods

        public string GetAutoCompleteSaveDir()
        {
            return Directory.GetCurrentDirectory();
        }

        public string GetRegistryProgId()
        {
            return ProgId;
        }

        #endregion

    }
}
