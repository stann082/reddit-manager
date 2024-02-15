namespace lib;

public interface IOptions
{
    
    // attributes
    bool Cache { get; }
    bool GetCached { get; }
    bool Comment { get; }
    string Query { get; }
    string Filter { get; }
    int Limit { get; }
    string User { get; }

    // behavior
    string GetFilterValue(string key);
    
}
