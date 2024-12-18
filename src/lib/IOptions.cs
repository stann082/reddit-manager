namespace lib;

public interface IOptions
{
    
    string Author { get; }
    
    bool IsArchive { get; }
    string Id { get; }
    bool IsDescending { get; }
    bool IsExactWord { get; }
    bool IsFilterEnabled { get; }
    
    int Limit { get; }
    
    string Query { get; }
    
    bool SortByDate { get; }
    bool SortByScore { get; }
    bool ShowId { get; }
    bool ShouldExport { get; }
    public DateTime StartDate { get; }
    public DateTime StopDate { get; }
    string Subreddit { get; }
    
}
