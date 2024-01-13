using Reddit.Controllers;

namespace lib;

public interface ISearchService
{
    Task<Post[]> Search(ISearchOptions options);
}
