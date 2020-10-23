using Domain;
using NUnit.Framework;

namespace DomainTest
{
    [TestFixture]
    public class RedditDataTest
    {

        private RedditData Data;

        [SetUp]
        public void SetUp()
        {
            Data = new RedditData();
        }

        #region Blue Sky Tests

        [Test]
        public void TestBlueSky_Coverage()
        {
            // pre-conditions
            Assert.IsNull(Data.Contents);

            // exercise
            Data.Contents = new RedditInfo[] { new RedditInfo() };

            // post-conditions
            Assert.AreEqual(1, Data.Contents.Length);
        }

        #endregion

        [TearDown]
        public void TearDown()
        {

        }

    }
}
