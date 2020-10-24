using Microsoft.Win32;
using System;
using System.IO;

namespace Domain
{
    public class EnvironmentProperties : IEnvironmentProperties
    {

        #region Properties

        public static string AutoCompleteSaveDir { get { return EnvironmentPropertiesProviderProxy.Singleton.GetAutoCompleteSaveDir(); } }
        public static string RegistryProgId { get { return EnvironmentPropertiesProviderProxy.Singleton.GetRegistryProgId(); } }

        #endregion

        #region Public Methods

        public string GetAutoCompleteSaveDir()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appDataPath, "PushshiftAPI", "autocomplete");
        }

        public string GetRegistryProgId()
        {
            const string userChoice = @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice";
            RegistryKey userChoiceKey = Registry.CurrentUser.OpenSubKey(userChoice);
            if (userChoiceKey == null)
            {
                return string.Empty;
            }

            object progIdValue = userChoiceKey.GetValue("Progid");
            if (progIdValue == null)
            {
                return string.Empty;
            }

            return progIdValue.ToString();
        }

        #endregion

    }
}
