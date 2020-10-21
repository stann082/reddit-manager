using NUnit.Framework;
using Presentation;

namespace PresentationTest
{
    [TestFixture]
    public class PushshiftApiPresenterTest
    {

        private PushshiftApiPresenter Presenter;

        [SetUp]
        public void SetUp()
        {
            Presenter = new PushshiftApiPresenter();
        }

        #region Blue Sky Tests

        [Test]
        public void TestBlueSky_BuildResponseContent()
        {
            Assert.IsNotNull(Presenter);
        }

        #endregion

    }
}
