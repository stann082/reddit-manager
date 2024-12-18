using lib;

namespace web.Data;

public class WebOptions : IOptions
{

    #region Properties

    public string Author { get; set; } = string.Empty;

    public bool IsArchive { get; set; } = false;
    public string Id { get; set; } = string.Empty;
    public bool IsDescending { get; set; } = false;
    public bool IsExactWord { get; set; } = false;

    public bool IsFilterEnabled =>
        !string.IsNullOrEmpty(Author) || !string.IsNullOrEmpty(Subreddit) || !string.IsNullOrEmpty(Query);

    public int Limit { get; set; } = 25;

    public string Query { get; set; } = string.Empty;
    public int ScoreGreaterThan { get; set; } = int.MinValue;
    public int ScoreLessThan { get; set; } = int.MaxValue;

    public bool ShowId { get; set; } = false;
    public bool ShouldExport { get; set; } = false;
    public DateTime StartDate { get; set; } = new DateTime(2012, 1, 1);
    public DateTime StopDate { get; set; } = DateTime.Today;
    public string Subreddit { get; set; } = string.Empty;

    #endregion

}