using lib;

namespace ui.Data;

public class Options : IOptions
{

    #region Properties

    public string Author { get; set; }
    
    public bool IsArchive { get; set; }
    public string Id { get; set; }
    public bool IsExactWord { get; set; }
    public bool IsFilterEnabled =>
        !string.IsNullOrEmpty(Author) || !string.IsNullOrEmpty(Subreddit) || !string.IsNullOrEmpty(Query);
    
    public int Limit { get; set; } = 25;
    
    public string Query { get; set; }
    
    public bool ShowId { get; set; }
    public bool ShouldExport { get; set; }
    public string Subreddit { get; set; }
    
    #endregion

}