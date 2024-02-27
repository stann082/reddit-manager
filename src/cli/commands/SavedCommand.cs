using lib;

namespace cli.commands;

public class SavedCommand : AbstractCommand
{

    #region Constructors

    public SavedCommand(IOptions options, ISavedService service)
        : base(options)
    {
        _service = service;
    }

    #endregion

    #region Variables

    private readonly ISavedService _service;

    #endregion

    #region Overriden Methods

    protected override Task<CommentPreview[]> GetComments(IOptions options)
    {
        return _service.GetFilteredItemsAsync(options);
    }

    #endregion

}
