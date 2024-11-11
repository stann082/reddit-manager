using lib;

namespace cli.commands;

public class SearchCommand(IOptions options, ISearchService service) : AbstractCommand(options)
{

    #region Overriden Methods

    protected override Task<CommentModel[]> GetAllComments()
    {
        // does not apply to search
        return Task.FromResult(Array.Empty<CommentModel>());
    }

    protected override Task<CommentPreview[]> GetFilteredComments(IOptions options)
    {
        return service.Search(options);
    }

    #endregion

}
