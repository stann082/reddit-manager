using lib.options;

namespace lib.services;

public interface ISearchService
{
    Task<CommentPreview[]> Search(IOptions savedOptions);
}
