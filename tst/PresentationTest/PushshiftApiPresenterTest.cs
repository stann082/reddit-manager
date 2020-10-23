using Domain;
using NUnit.Framework;
using Presentation;
using TestEnvironment;

namespace PresentationTest
{
    [TestFixture]
    public class PushshiftApiPresenterTest : AbstractPresentationTest
    {

        private PushshiftApiPresenter Presenter;
        private MockRedditApiService MockService;

        [SetUp]
        public void SetUp()
        {
            MockService = ServiceFactoryProxy.Singleton.CreateRedditService() as MockRedditApiService;
            MockService.Data = CreateMockRedditData();

            Presenter = new PushshiftApiPresenter();
        }

        #region Blue Sky Tests

        [Test]
        public void TestBlueSky_BuildResponseContent()
        {
            // exercise
            Presenter.BuildResponseContent(new MockSearchOptions()).Wait();

            // post-conditions
            Assert.IsNotEmpty(Presenter.Response);
            Assert.AreEqual("Showing 2 items", Presenter.Counter);
        }

        [Test]
        public void TestBlueSky_BuildResponseContent_ShowExactMatches()
        {
            // set-up
            MockSearchOptions options = new MockSearchOptions();
            options.Query = "Test and";
            options.ShowExactMatches = true;

            // exercise
            Presenter.BuildResponseContent(options).Wait();

            // post-conditions
            Assert.IsNotEmpty(Presenter.Response);
            Assert.AreEqual("Showing 1 items", Presenter.Counter);
        }

        #endregion

        #region Non Blue Sky Tests

        [Test]
        public void TestNonBlueSky_BuildResponseContent_NothingFound()
        {
            // set-up
            MockService.Data.Contents = new RedditInfo[0];

            MockSearchOptions options = new MockSearchOptions();
            options.Query = "Test and";
            options.ShowExactMatches = true;

            // exercise
            Presenter.BuildResponseContent(options).Wait();

            // post-conditions
            Assert.AreEqual("Nothing found...", Presenter.Response);
            Assert.IsNull(Presenter.Counter);
        }

        [Test]
        public void TestNonBlueSky_BuildResponseContent_SomethingWentWrong()
        {
            // set-up
            MockService.Data = null;

            MockSearchOptions options = new MockSearchOptions();
            options.Query = "Test and";
            options.ShowExactMatches = true;

            // exercise
            Presenter.BuildResponseContent(options).Wait();

            // post-conditions
            Assert.AreEqual("Something went wrong...", Presenter.Response);
            Assert.IsNull(Presenter.Counter);
        }

        #endregion

        [TearDown]
        public void TearDown()
        {

        }

        #region Helper Methods

        private RedditData CreateMockRedditData()
        {
            RedditData data = new RedditData();

            RedditInfo info1 = new RedditInfo();
            info1.Author = "Author #1";
            info1.Body = "Test message #1";
            info1.CreatedUtc = 1603480418;
            info1.RetrievedOn = 1603483346;
            info1.Score = 1;
            info1.Subreddit = "Test";

            RedditInfo info2 = new RedditInfo();
            info2.Author = "Author #2";
            info2.Body = "Test and message #2";
            info2.CreatedUtc = 1603480287;
            info2.RetrievedOn = 1603483194;
            info2.Score = 5;
            info2.Subreddit = "Test";
            info2.UpdatedUtc = 1603483194;

            data.Contents = new RedditInfo[] { info1, info2 };
            return data;
        }

        #endregion

    }
}
