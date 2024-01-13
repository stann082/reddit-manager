using Reddit.Things;

namespace lib;

public interface ISearchService
{
    Task<Comment[]> Search(ISearchOptions savedOptions);
}
