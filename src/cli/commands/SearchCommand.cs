using lib;

namespace cli.commands;

public class SearchCommand(IOptions options, ISearchService service) : AbstractCommand(options)
{

    #region Overriden Methods

    protected override Task<CommentPreview[]> GetComments(IOptions options)
    {
        return service.Search(options);
    }

    #endregion

}
