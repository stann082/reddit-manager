using Domain;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using System.Threading.Tasks;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{

    #region Constructors

    public SearchController(IRedditApiService service)
    {
        Service = service;
    }

    #endregion

    #region Variables

    private readonly IRedditApiService Service;

    #endregion

    #region Endpoints

    [HttpPost]
    [Route("/search")]
    public async Task<IRedditData> Search([FromBody] SearchRequestModel model)
    {
        SearchPresenter presenter = new SearchPresenter(Service);
        return await presenter.GetData(model);
    }

    #endregion

}
