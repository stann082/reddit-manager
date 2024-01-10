namespace lib;

public interface IOptions
{
    bool Comment { get; }
    string Filter { get; }
    bool Post { get; }
    string Query { get; }
}
