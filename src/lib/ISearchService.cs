using System.Collections.Generic;
using System.Threading.Tasks;

namespace lib;

public interface ISearchService
{
    Task<(CommentPreview[] Comments, string NextAfter)> SearchCommentsAsync(IOptions options);
    
    Task<List<CommentModel>> GetThreadCommentsAsync(string threadId);
}
