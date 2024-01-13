namespace cli.commands;

public abstract class AbstractCommand
{

    #region Constructors

    protected AbstractCommand(string username)
    {
        string myRedditUsername = Environment.GetEnvironmentVariable("MY_REDDIT_USERNAME");
        Username = !string.IsNullOrEmpty(username) ? username : myRedditUsername;
    }

    #endregion

    #region Porperties

    protected string Username { get; }

    #endregion
    
}
