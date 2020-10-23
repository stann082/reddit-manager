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
        public async Task TestBlueSky_GetRedditData()
        {
            // exercise
            string requestUri = "?q=test&subreddit=AskReddit&size=5&sort_type=created_utc&sort=desc";
            RedditData data = await Service.GetRedditData(requestUri);

            // post-conditions
            Assert.AreEqual(5, data.Contents.Length);
        }

        [Test]
        public async Task TestBlueSky_GetRedditData_NothingFound()
        {
            // exercise
            string requestUri = "?q=dfjdshdfdsjfsdlfdsfcjdslhff87487r48ch&subreddit=AskReddit&size=5&sort_type=created_utc&sort=desc";
            RedditData data = await Service.GetRedditData(requestUri);

            // post-conditions
            Assert.IsEmpty(data.Contents);
        }

        #endregion

        #region Non Blue Sky Tests

        [Test]
        public async Task TestNonBlueSky_GetRedditData_BadUri()
        {
            // exercise
            RedditData data = await Service.GetRedditData("bleh");

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
