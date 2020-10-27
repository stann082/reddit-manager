using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Domain
{
    public class ApplicationEnvironment
    {

        public static ApplicationEnvironment Singleton = new ApplicationEnvironment();

        #region Constructors

        private ApplicationEnvironment()
        {
            SavedQueries = Array.Empty<string>();
            SavedSubreddits = Array.Empty<string>();
            SavedUserNames = Array.Empty<string>();
        }

        #endregion

        #region Properties

        public string[] SavedQueries { get; private set; }
        public string[] SavedSubreddits { get; private set; }
        public string[] SavedUserNames { get; private set; }

        public string WebBrowserFilePath { get; private set; }

        private string AutoCompleteQueryFilePath { get; set; }
        private string AutoCompleteSaveDir { get; set; }
        private string AutoCompleteSubredditFilePath { get; set; }
        private string AutoCompleteUserNameFilePath { get; set; }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            InitializeAutoCompleteSaves();
            InitializeWebBrowserFilePath();
        }

        public void InitializeAutoCompleteSaves()
        {
            AutoCompleteSaveDir = EnvironmentProperties.AutoCompleteSaveDir;
            if (!Directory.Exists(AutoCompleteSaveDir))
            {
                Directory.CreateDirectory(AutoCompleteSaveDir);
            }

            AutoCompleteQueryFilePath = Path.Combine(AutoCompleteSaveDir, "query");
            if (File.Exists(AutoCompleteQueryFilePath))
            {
                SavedQueries = File.ReadAllLines(AutoCompleteQueryFilePath);
            }

            AutoCompleteSubredditFilePath = Path.Combine(AutoCompleteSaveDir, "subreddit");
            if (File.Exists(AutoCompleteSubredditFilePath))
            {
                SavedSubreddits = File.ReadAllLines(AutoCompleteSubredditFilePath);
            }

            AutoCompleteUserNameFilePath = Path.Combine(AutoCompleteSaveDir, "username");
            if (File.Exists(AutoCompleteUserNameFilePath))
            {
                SavedUserNames = File.ReadAllLines(AutoCompleteUserNameFilePath);
            }
        }

        public void SaveAutoCompletes(ISearchOptions options)
        {
            List<string> savedQueries = SavedQueries.ToList();
            if (savedQueries.Contains(options.Query))
            {
                savedQueries.Remove(options.Query);
            }

            savedQueries.Add(options.Query);
            SavedQueries = savedQueries.ToArray();
            File.WriteAllLines(AutoCompleteQueryFilePath, SavedQueries);

            List<string> savedUserNames = SavedUserNames.ToList();
            if (savedUserNames.Contains(options.UserName))
            {
                savedUserNames.Remove(options.UserName);
            }

            savedUserNames.Add(options.UserName);
            SavedUserNames = savedUserNames.ToArray();
            File.WriteAllLines(AutoCompleteUserNameFilePath, SavedUserNames);

            List<string> savedSubreddits = SavedSubreddits.ToList();
            if (savedSubreddits.Contains(options.Subreddit))
            {
                savedSubreddits.Remove(options.Subreddit);
            }

            savedSubreddits.Add(options.Subreddit);
            SavedSubreddits = savedSubreddits.ToArray();
            File.WriteAllLines(AutoCompleteSubredditFilePath, SavedSubreddits);
        }

        #endregion

        #region Helper Methods

        private void InitializeWebBrowserFilePath()
        {
            // based on https://stackoverflow.com/a/17599201

            switch (EnvironmentProperties.RegistryProgId)
            {
                case "IE.HTTP":
                    WebBrowserFilePath = "iexplore.exe";
                    break;
                case "FirefoxURL":
                    WebBrowserFilePath = "firefox.exe";
                    break;
                case "ChromeHTML":
                    WebBrowserFilePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
                    break;
                case "OperaStable":
                    WebBrowserFilePath = "opera.exe";
                    break;
                case "SafariHTML":
                    WebBrowserFilePath = "safari.exe";
                    break;
                case "AppXq0fevzme2pys62n3e0fbqa7peapykr8v":
                    WebBrowserFilePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
                    break;
                default:
                    WebBrowserFilePath = string.Empty;
                    break;
            }
        }

        #endregion

    }
}
