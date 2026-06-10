using System.Threading.Tasks;

namespace lib;

public interface IRedditClient
{
    Task<string> GetJsonAsync(string pathOrUrl);
    Task<string> LogIn();
}
