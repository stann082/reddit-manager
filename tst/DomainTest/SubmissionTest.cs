using Domain;
using NUnit.Framework;

namespace DomainTest
{
    public class SubmissionTest
    {

        private Submission Info;

        [SetUp]
        public void SetUp()
        {
            Info = new Submission();
        }

        #region Blue Sky Tests

        [Test]
        public void TestBlueSky_Coverage()
        {
            // pre-conditions
            Assert.IsNull(Info.Author);
            Assert.AreEqual(0, Info.CreatedUtc);
            Assert.IsNull(Info.Id);
            Assert.IsFalse(Info.NoFollow);
            Assert.IsNull(Info.Permalink);
            Assert.AreEqual(0, Info.RetrievedOn);
            Assert.AreEqual(0, Info.Score);
            Assert.IsFalse(Info.SendReplies);
            Assert.IsFalse(Info.Stickied);
            Assert.IsNull(Info.Subreddit);
            Assert.IsNull(Info.SubredditId);

            // exercise
            Info.Author = "Author";
            Info.CreatedUtc = 1688755646;
            Info.Id = "Id";
            Info.Locked = true;
            Info.NoFollow = true;
            Info.Permalink = "Permalink";
            Info.RetrievedOn = 1688755646;
            Info.Score = 10;
            Info.SendReplies = true;
            Info.Stickied = true;
            Info.Subreddit = "Subreddit";
            Info.SubredditId = "SubredditId";

            // post-conditions
            Assert.AreEqual("Author", Info.Author);
            Assert.AreEqual(1688755646, Info.CreatedUtc);
            Assert.AreEqual("Id", Info.Id);
            Assert.IsTrue(Info.Locked);
            Assert.IsTrue(Info.NoFollow);
            Assert.AreEqual("Permalink", Info.Permalink);
            Assert.AreEqual(1688755646, Info.RetrievedOn);
            Assert.AreEqual(10, Info.Score);
            Assert.IsTrue(Info.SendReplies);
            Assert.IsTrue(Info.Stickied);
            Assert.AreEqual("Subreddit", Info.Subreddit);
            Assert.AreEqual("SubredditId", Info.SubredditId);
        }

        #endregion

        [TearDown]
        public void TearDown()
        {

        }

    }
}
