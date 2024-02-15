using lib;
using Reddit.Things;

namespace cli.commands;

public class SearchCommand : AbstractCommand
{

    #region Constructors

    public SearchCommand(IOptions options, ISearchService service)
        : base(options)
    {
        _service = service;
    }

    #endregion

    #region Variables

    private readonly ISearchService _service;

    #endregion

    #region Overriden Methods

    protected override Task Cache()
    {
        throw new NotImplementedException();
    }

    protected override Task<Comment[]> GetComments(IOptions options)
    {
        return _service.Search(options);
    }

    #endregion
    
}
