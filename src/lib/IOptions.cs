namespace lib;

public interface IOptions
{
    
    string Author { get; }
    
    bool IsArchive { get; }
    string Id { get; }
    bool IsExactWord { get; }
    bool IsFilterEnabled { get; }
    
    int Limit { get; }
    
    string Query { get; }
    
    bool ShowId { get; }
    bool ShouldExport { get; }
    string Subreddit { get; }
    
}
