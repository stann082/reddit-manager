namespace lib;

public interface IAuthOptions
{
    string AppId { get; }
    string AppSecret { get; }
    int Port { get; }
}
