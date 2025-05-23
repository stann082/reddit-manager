using System.Threading.Tasks;

namespace lib;

public interface ISavedService
{
    Task<CommentModel[]> GetAllItemsAsync();
    Task<(CommentPreview[], int)> GetFilteredItemsAsync(IOptions options);
}
