using Domain;
using NUnit.Framework;

namespace DomainTest
{
    [TestFixture]
    public class CommentTest
    {

        private Comment Info;

        [SetUp]
        public void SetUp()
        {
            Info = new Comment();
        }

        #region Blue Sky Tests

        [Test]
        public void TestBlueSky_Coverage()
        {
            // pre-conditions
            Assert.IsNull(Info.Author);
            Assert.IsNull(Info.AuthorFullName);
            Assert.IsNull(Info.Message);
            Assert.AreEqual(0, Info.CreatedUtc);
            Assert.IsNull(Info.Id);
            Assert.IsFalse(Info.IsSubmitter);
            Assert.IsNull(Info.LinkId);
            Assert.IsFalse(Info.Locked);
            Assert.IsFalse(Info.NoFollow);
            Assert.IsNull(Info.ParentId);
            Assert.IsNull(Info.Permalink);
            Assert.AreEqual(0, Info.RetrievedOn);
            Assert.AreEqual(0, Info.Score);
            Assert.IsFalse(Info.SendReplies);
            Assert.IsFalse(Info.Stickied);
            Assert.IsNull(Info.Subreddit);
            Assert.IsNull(Info.SubredditId);
            Assert.AreEqual(0, Info.TotalAwardsReceived);
            Assert.IsNull(Info.UpdatedUtc);

            // exercise
            Info.Author = "Author";
            Info.AuthorFullName = "AuthorFullName";
            Info.Message = "Body";
            Info.CreatedUtc = 1688755646;
            Info.Id = "Id";
            Info.IsSubmitter = true;
            Info.LinkId = "LinkId";
            Info.Locked = true;
            Info.NoFollow = true;
            Info.ParentId = "ParentId";
            Info.Permalink = "Permalink";
            Info.RetrievedOn = 1688755646;
            Info.Score = 10;
            Info.SendReplies = true;
            Info.Stickied = true;
            Info.Subreddit = "Subreddit";
            Info.SubredditId = "SubredditId";
            Info.TotalAwardsReceived = 5;
            Info.UpdatedUtc = 1688755646;

            // post-conditions
            Assert.AreEqual("Author", Info.Author);
            Assert.AreEqual("AuthorFullName", Info.AuthorFullName);
            Assert.AreEqual("Body", Info.Message);
            Assert.AreEqual(1688755646, Info.CreatedUtc);
            Assert.AreEqual("Id", Info.Id);
            Assert.IsTrue(Info.IsSubmitter);
            Assert.AreEqual("LinkId", Info.LinkId);
            Assert.IsTrue(Info.Locked);
            Assert.IsTrue(Info.NoFollow);
            Assert.AreEqual("ParentId", Info.ParentId);
            Assert.AreEqual("Permalink", Info.Permalink);
            Assert.AreEqual(1688755646, Info.RetrievedOn);
            Assert.AreEqual(10, Info.Score);
            Assert.IsTrue(Info.SendReplies);
            Assert.IsTrue(Info.Stickied);
            Assert.AreEqual("Subreddit", Info.Subreddit);
            Assert.AreEqual("SubredditId", Info.SubredditId);
            Assert.AreEqual(5, Info.TotalAwardsReceived);
            Assert.AreEqual(1688755646, Info.UpdatedUtc);
        }

        #endregion

        [TearDown]
        public void TearDown()
        {

        }

    }
}
