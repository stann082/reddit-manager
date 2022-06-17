using Domain;
using NUnit.Framework;
using TestEnvironment;

namespace DomainTest
{
    [TestFixture]
    public class CommentDataTest : AbstractTest
    {

        private CommentData Data;

        [SetUp]
        public void SetUp()
        {
            Data = new CommentData();
        }

        #region Blue Sky Tests

        [Test]
        public void TestBlueSky_Coverage()
        {
            // pre-conditions
            Assert.IsEmpty(Data.Contents);

            // exercise
            OverrideProperty(Data, "Comments", new Comment[] { new() });

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
