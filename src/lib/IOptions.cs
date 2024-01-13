namespace lib;

public interface IOptions
{
    
    // attributes
    string Query { get; }
    string Filter { get; }

    // behavior
    string GetFilterValue(string key);
    
}
