using Domain;
using Microsoft.AspNetCore.Mvc;
using Presentation;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class RedditController : ControllerBase
{

    #region Constructors

    public RedditController(IRedditApiService service)
    {
        Service = service;
    }

    #endregion

    #region Variables

    private IRedditApiService Service;

    #endregion

    #region Endpoints

    [HttpPost(Name = "Search")]
    public async Task<IRedditData> Search([FromBody] SearchRequestModel model)
    {
        SearchPresenter presenter = new SearchPresenter(Service);
        return await presenter.GetData(model);
    }

    #endregion

}
