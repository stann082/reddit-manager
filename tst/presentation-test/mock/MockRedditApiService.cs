using Domain;
using System.Threading.Tasks;

namespace PresentationTest
{
    public class MockRedditApiService : NullRedditApiService
    {

        #region Public Methods

        public CommentData CommentData { get; set; }
        public SubmissionData SubmissionData { get; set; }

        #endregion

        #region Public Methods

        public override Task<IRedditData> GetCommentData(string requestUri)
        {
            return Task<IRedditData>.Factory.StartNew(() => CommentData);
        }

        public override Task<IRedditData> GetSubmissionData(string requestUri)
        {
            return Task<IRedditData>.Factory.StartNew(() => SubmissionData);
        }

        #endregion

    }
}
