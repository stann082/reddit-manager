namespace lib;

public interface ISavedOptions : IOptions
{
    bool Comment { get; }
    bool Post { get; }
    string User { get; }
}
