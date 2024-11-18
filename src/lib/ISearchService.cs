namespace lib;

public interface ISearchService
{
    Task<(CommentPreview[], int)> Search(IOptions savedOptions);
}
