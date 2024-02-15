namespace lib;

public interface ISearchService
{
    Task<CommentPreview[]> Search(IOptions savedOptions);
}
