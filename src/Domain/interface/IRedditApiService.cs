using System.Threading.Tasks;

namespace Domain
{
    public interface IRedditApiService
    {

        Task<IRedditData> GetCommentData(string requestUri);
        Task<IRedditData> GetSubmissionData(string requestUri);

    }
}
