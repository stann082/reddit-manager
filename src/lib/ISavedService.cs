using System.Threading.Tasks;
using lib.options;

namespace lib;

public interface ISavedService
{
    Task<CommentModel[]> GetAllItemsAsync();
    Task<CommentModel[]> GetCommentsFromPushshiftArchive(CacheOptions options);
    Task<(CommentPreview[], int)> GetFilteredItemsAsync(IOptions options);
}
