namespace lib.options;

public interface IAuthOptions
{
    string AppId { get; }
    string AppSecret { get; }
    int Port { get; }
}
