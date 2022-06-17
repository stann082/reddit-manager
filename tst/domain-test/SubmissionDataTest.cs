using Domain;
using NUnit.Framework;
using TestEnvironment;

namespace DomainTest
{
    [TestFixture]
    public class SubmissionDataTest : AbstractTest
    {

        private SubmissionData Data;

        [SetUp]
        public void SetUp()
        {
            Data = new SubmissionData();
        }

        #region Blue Sky Tests

        [Test]
        public void TestBlueSky_Coverage()
        {
            // pre-conditions
            Assert.IsEmpty(Data.Contents);

            // exercise
            OverrideProperty(Data, "Submissions", new Submission[] { new() });

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
