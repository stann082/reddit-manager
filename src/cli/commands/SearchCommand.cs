using lib;
using Serilog;

namespace cli.commands;

public class SearchCommand(IOptions options, ISearchService service) : AbstractCommand(options)
{

    #region Overriden Methods

    protected override Task<CommentModel[]> GetAllComments()
    {
        // does not apply to search
        return Task.FromResult(Array.Empty<CommentModel>());
    }

    protected override Task<(CommentPreview[], int)> GetFilteredComments(IOptions options)
    {
        Log.Debug("Fetching comments from Reddit API with {@Options}", options);
        return service.Search(options);
    }

    #endregion

}
