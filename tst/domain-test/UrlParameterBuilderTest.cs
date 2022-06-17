using Domain;
using NUnit.Framework;
using TestEnvironment;

namespace DomainTest
{
    [TestFixture]
    public class UrlParameterBuilderTest
    {

        private MockSearchOptions Options;
        private UrlParameterBuilder Builder;

        [SetUp]
        public void SetUp()
        {
            Options = new MockSearchOptions();
        }

        #region Blue Sky Tests

        [Test]
        public void TestBlueSky_Build()
        {
            // set-up
            Options.Query = "just a test";
            Options.UserName = "Author";
            Options.Subreddit = "Test";
            Options.TotalResults = "5";
            Options.IsPeriodSearchEnabled = true;
            Options.ScoreGreaterThan = "10";
            Options.ScoreLessThan = "20";
            Options.StartDateUnixTimeStamp = 1603480418;
            Options.StopDateUnixTimeStamp = 1603483346;
            Options.SortDirection = "desc";
            Options.SortType = "score";
            Builder = new UrlParameterBuilder(Options);

            // exercise
            string requestUri = Builder.Build("comment");

            // post-conditions
            string expectedUri = "comment?q=just+a+test&author=Author&subreddit=Test&size=5&after=1603480418&before=1603483346&score=>10&score=<20&sort_type=score&sort=desc";
            Assert.AreEqual(expectedUri, requestUri);
        }

        #endregion

        [TearDown]
        public void TearDown()
        {

        }

    }
}
