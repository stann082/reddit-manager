using System.Threading.Tasks;

namespace lib;

public interface ISearchService
{
    Task<(CommentPreview[] Comments, string NextAfter)> SearchCommentsAsync(IOptions options); 
}
