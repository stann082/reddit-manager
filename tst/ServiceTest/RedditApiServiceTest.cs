using Domain;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ServiceTest
{
    [TestFixture]
    public class RedditApiServiceTest : AbstractServiceTest
    {

        [SetUp]
        public void SetUp()
        {
            Service = ServiceFactoryProxy.Singleton.CreateRedditService();
        }

        private IRedditApiService Service;

        #region Blue Sky Tests

        [Test]
        public async Task TestBlueSky_GetCommentData()
        {
            // exercise
            string requestUri = "?q=test&subreddit=AskReddit&size=5&sort_type=created_utc&sort=desc";
            IRedditData data = await Service.GetCommentData(requestUri);

            // post-conditions
            Assert.AreEqual(5, data.Contents.Length);
        }

        [Test]
        public async Task TestBlueSky_GetCommentData_NothingFound()
        {
            // exercise
            string requestUri = "?q=dfjdshdfdsjfsdlfdsfcjdslhff87487r48ch&subreddit=AskReddit&size=5&sort_type=created_utc&sort=desc";
            IRedditData data = await Service.GetCommentData(requestUri);

            // post-conditions
            Assert.IsEmpty(data.Contents);
        }

        [Test]
        public async Task TestBlueSky_GetSubmissionData()
        {
            // exercise
            string requestUri = "?q=test&subreddit=AskReddit&size=5&sort_type=created_utc&sort=desc";
            IRedditData data = await Service.GetSubmissionData(requestUri);

            // post-conditions
            Assert.AreEqual(5, data.Contents.Length);
        }

        #endregion

        #region Non Blue Sky Tests

        [Test]
        public async Task TestNonBlueSky_GetCommentData_BadUri()
        {
            // exercise
            IRedditData data = await Service.GetCommentData("bleh");

            // post-conditions
            Assert.IsNull(data);
        }

        #endregion

        [TearDown]
        public void TearDown()
        {

        }

    }
}
