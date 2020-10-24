using Domain;
using NUnit.Framework;
using System.IO;
using TestEnvironment;

namespace DomainTest
{
    [TestFixture]
    public class ApplicationEnvironmentTest
    {

        private MockEnvironmentProperties MockEnvironmentProperties;
        private ApplicationEnvironment Environment;

        [SetUp]
        public void SetUp()
        {
            MockEnvironmentProperties = new MockEnvironmentProperties();
            EnvironmentPropertiesProviderProxy.Singleton = MockEnvironmentProperties;
            Environment = ApplicationEnvironment.Singleton;
        }

        #region Blue Sky Tests

        [Test]
        public void TestBlueSky_InitializeWebBrowserFilePath()
        {
            // pre-conditions
            Assert.IsNull(Environment.WebBrowserFilePath);

            // exercise
            MockEnvironmentProperties.ProgId = "IE.HTTP";
            Environment.Initialize();

            // mid-conditions
            Assert.AreEqual("iexplore.exe", Environment.WebBrowserFilePath);

            // exercise
            MockEnvironmentProperties.ProgId = "FirefoxURL";
            Environment.Initialize();

            // mid-conditions
            Assert.AreEqual("firefox.exe", Environment.WebBrowserFilePath);

            // exercise
            MockEnvironmentProperties.ProgId = "ChromeHTML";
            Environment.Initialize();

            // mid-conditions
            Assert.AreEqual(@"C:\Program Files\Google\Chrome\Application\chrome.exe", Environment.WebBrowserFilePath);

            // exercise
            MockEnvironmentProperties.ProgId = "OperaStable";
            Environment.Initialize();

            // mid-conditions
            Assert.AreEqual("opera.exe", Environment.WebBrowserFilePath);

            // exercise
            MockEnvironmentProperties.ProgId = "SafariHTML";
            Environment.Initialize();

            // mid-conditions
            Assert.AreEqual("safari.exe", Environment.WebBrowserFilePath);

            // exercise
            MockEnvironmentProperties.ProgId = "AppXq0fevzme2pys62n3e0fbqa7peapykr8v";
            Environment.Initialize();

            // post-conditions
            Assert.AreEqual(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe", Environment.WebBrowserFilePath);
        }

        [Test]
        public void TestBlueSky_SaveAutoCompletes()
        {
            // set-up
            CreateFile("query", new string[] { "this", "is", "just", "test" });
            CreateFile("subreddit", new string[] { "askreddit", "news", "politics" });
            CreateFile("username", new string[] { "user1", "user2" });
            Environment.Initialize();

            // pre-conditions
            Assert.AreEqual(4, Environment.SavedQueries.Length);
            Assert.AreEqual(3, Environment.SavedSubreddits.Length);
            Assert.AreEqual(2, Environment.SavedUserNames.Length);

            // exercise
            MockSearchOptions options = new MockSearchOptions();
            options.Query = "and";
            options.Subreddit = "funny";
            options.UserName = "user3";
            Environment.SaveAutoCompletes(options);
            Environment.Initialize();

            // mid-conditions
            Assert.AreEqual(5, Environment.SavedQueries.Length);
            Assert.AreEqual(4, Environment.SavedSubreddits.Length);
            Assert.AreEqual(3, Environment.SavedUserNames.Length);

            // exercise - add already existing items
            Environment.SaveAutoCompletes(options);
            Environment.Initialize();

            // mid-conditions
            Assert.AreEqual(5, Environment.SavedQueries.Length);
            Assert.AreEqual(4, Environment.SavedSubreddits.Length);
            Assert.AreEqual(3, Environment.SavedUserNames.Length);
        }

        #endregion

        #region Non Blue Sky Tests

        [Test]
        public void TestNonBlueSky_InitializeWebBrowserFilePath_NoRegistryProdId()
        {
            // exercise
            MockEnvironmentProperties.ProgId = "bleh";
            Environment.Initialize();

            // post-conditions
            Assert.IsEmpty(Environment.WebBrowserFilePath);
        }

        #endregion

        [TearDown]
        public void TearDown()
        {
            RemoveFile("query");
            RemoveFile("subreddit");
            RemoveFile("username");
        }

        #region Helper Methods

        private void CreateFile(string autoCompleteDir, string[] fileEntries)
        {
            string fullPath = Path.Combine(EnvironmentProperties.AutoCompleteSaveDir, autoCompleteDir);
            File.WriteAllLines(fullPath, fileEntries);
        }

        private void RemoveFile(string autoCompleteDir)
        {
            string fullPath = Path.Combine(EnvironmentProperties.AutoCompleteSaveDir, autoCompleteDir);
            if (!File.Exists(fullPath))
            {
                return;
            }

            File.Delete(fullPath);
        }

        #endregion

    }
}
