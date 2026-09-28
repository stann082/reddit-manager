using System.Collections.Generic;
using System.Threading.Tasks;

namespace lib;

public interface ISearchService
{
    Task<(CommentPreview[] Comments, int Total)> SearchCommentsAsync(IOptions options);
    
    Task<List<CommentModel>> GetThreadCommentsAsync(string threadId);
}
