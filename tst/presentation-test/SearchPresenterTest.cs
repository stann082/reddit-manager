using Domain;
using NUnit.Framework;
using Presentation;
using TestEnvironment;

namespace PresentationTest
{
    [TestFixture]
    public class SearchPresenterTest : AbstractPresentationTest
    {

        private SearchPresenter Presenter;
        private MockRedditApiService MockService;

        [SetUp]
        public void SetUp()
        {
            MockService = new MockRedditApiService();
            MockService.CommentData = CreateMockRedditData();
            Presenter = new SearchPresenter(MockService);
        }

        #region Blue Sky Tests

        [Test]
        public void TestBlueSky_BuildResponseContent()
        {
            // exercise
            Presenter.GetData(new MockSearchOptions()).Wait();

            // post-conditions
            Assert.IsNotEmpty(Presenter.Response);
            Assert.AreEqual("Showing 2 items", Presenter.Counter);
        }

        [Test]
        public void TestBlueSky_BuildResponseContent_ShowExactMatches()
        {
            // set-up
            MockSearchOptions options = new();
            options.Query = "Test and";
            options.ShowExactMatches = true;

            // exercise
            Presenter.GetData(options).Wait();

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
            OverrideProperty(MockService.CommentData, "Comments", new Comment[0]);

            MockSearchOptions options = new();
            options.Query = "Test and";
            options.ShowExactMatches = true;

            // exercise
            Presenter.GetData(options).Wait();

            // post-conditions
            Assert.AreEqual("Nothing found...", Presenter.Response);
            Assert.IsNull(Presenter.Counter);
        }

        [Test]
        public void TestNonBlueSky_BuildResponseContent_SomethingWentWrong()
        {
            // set-up
            MockService.CommentData = null;

            MockSearchOptions options = new();
            options.Query = "Test and";
            options.ShowExactMatches = true;

            // exercise
            Presenter.GetData(options).Wait();

            // post-conditions
            Assert.AreEqual("Something went wrong... Please check the logs", Presenter.Response);
            Assert.IsNull(Presenter.Counter);
        }

        #endregion

        [TearDown]
        public void TearDown()
        {

        }

        #region Helper Methods

        private CommentData CreateMockRedditData()
        {
            CommentData data = new();

            Comment info1 = new();
            info1.Author = "Author #1";
            info1.Message = "Test message #1";
            info1.CreatedUtc = 1603480418;
            info1.RetrievedOn = 1603483346;
            info1.Score = 1;
            info1.Subreddit = "Test";

            Comment info2 = new();
            info2.Author = "Author #2";
            info2.Message = "Test and message #2";
            info2.CreatedUtc = 1603480287;
            info2.RetrievedOn = 1603483194;
            info2.Score = 5;
            info2.Subreddit = "Test";
            info2.UpdatedUtc = 1603483194;

            OverrideProperty(data, "Comments", new Comment[] { info1, info2 });
            return data;
        }

        #endregion

    }
}
