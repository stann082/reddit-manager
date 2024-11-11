namespace lib;

public interface IOptions
{
    
    // attributes
    bool Comment { get; }
    bool IsExactWord { get; }
    string Filter { get; }
    int Limit { get; }
    string Query { get; }
    bool ShowId { get; }
    bool ShouldExport { get; }
    string User { get; }

    // behavior
    string GetFilterValue(string key);
    
}
