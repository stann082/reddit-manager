using System.Threading.Tasks;

namespace Domain
{
    public interface IRedditApiService
    {

        Task<RedditData> GetRedditData(string requestUri);

    }
}
