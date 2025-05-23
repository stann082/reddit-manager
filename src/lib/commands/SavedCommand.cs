using System.Threading.Tasks;
using Serilog;

namespace lib.commands;

public class SavedCommand(IOptions options, ISavedService service) : AbstractCommand(options)
{
    #region Overriden Methods

    protected override Task<CommentModel[]> GetAllComments()
    {
        return service.GetAllItemsAsync();
    }

    protected override Task<(CommentPreview[], int)> GetFilteredComments(IOptions options)
    {
        Log.Debug("Fetching locally saved comments with {@Options}", options);
        return service.GetFilteredItemsAsync(options);
    }

    #endregion
}