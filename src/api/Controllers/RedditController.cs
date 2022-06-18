using Domain;
using Microsoft.AspNetCore.Mvc;
using Presentation;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class RedditController : ControllerBase
{

    #region Endpoints

    [HttpPost(Name = "Search")]
    public async Task<IRedditData> Search([FromBody] object value)
    {
        SearchPresenter presenter = new SearchPresenter();
        return await presenter.GetRedditData(Constants.BASE_URL, QueryType.Comment);
    }

    #endregion

}
