using lib;
using Microsoft.AspNetCore.Mvc;
using Reddit.Things;

namespace web.Controllers;

[ApiController]
[Route("[controller]")]
public class SavedController : ControllerBase
{

    #region Fields and Properties

    private static readonly string[] Authors =
    {
        "Berglawn", "Bigashi", "BotQuant", "DavZeether", "Etrxiast", "EyeMyhero", "FashionNotice",
        "Followor", "Formock", "Ginove", "Guantopl", "Howorl", "Imeemra", "Intelecat", "Labser",
        "LinkinPanet", "Mantletec", "NearlySun", "OmahaEatsyou", "Perisin", "Persella", "PitcherHulk",
        "Racerno", "RocksFinal", "RosaBolt", "Roxam", "Scorinve", "TownDogg", "WarTeen", "WeblogTwit"
    };

    private readonly ILogger<SavedController> _logger;

    #endregion

    #region Constructors

    public SavedController(ILogger<SavedController> logger)
    {
        _logger = logger;
    }

    #endregion

    #region Actions or Endpoints

    [HttpGet]
    public IEnumerable<CommentPreview> Get()
    {
        return Enumerable.Range(1, 20).Select(_ => new CommentPreview(new Comment
        {
            Author = Authors[Random.Shared.Next(Authors.Length)],
            Body = "",
            Id = Guid.NewGuid().ToString(),
            Name = "",
            Permalink = "http://old.reddit.com",
            Score = Random.Shared.Next(-150, 1055),
            Subreddit = ""
        })).ToArray();
    }

    #endregion

}
