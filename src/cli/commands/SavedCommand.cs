using lib;

namespace cli.commands;

public class SavedCommand(IOptions options, ISavedService service) : AbstractCommand(options)
{

    #region Overriden Methods

    protected override Task<CommentModel[]> GetAllComments()
    {
        return service.GetAllItemsAsync();
    }

    protected override Task<CommentPreview[]> GetFilteredComments(IOptions options)
    {
        return service.GetFilteredItemsAsync(options);
    }

    #endregion

}
