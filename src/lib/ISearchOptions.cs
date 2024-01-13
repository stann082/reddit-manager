namespace lib;

public interface ISearchOptions : IOptions
{
    bool Comment { get; }
    bool Post { get; }
    string User { get; }
}
