namespace lib;

public class CacheCommand
{

    #region Constructors

    public CacheCommand(ICacheService service)
    {
        _service = service;
    }

    #endregion

    #region Variables

    private readonly ICacheService _service;

    #endregion

    #region Public Methods

    public async Task<int> Execute()
    {
        await _service.CacheSavedCommentsAsync();
        return await Task.FromResult(0);
    }

    #endregion

}
