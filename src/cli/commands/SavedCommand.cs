using lib;

namespace cli.commands;

public class SavedCommand(IOptions options, ISavedService service) : AbstractCommand(options)
{

    #region Overriden Methods

    protected override Task<CommentPreview[]> GetComments(IOptions options)
    {
        return service.GetFilteredItemsAsync(options);
    }

    #endregion

}
