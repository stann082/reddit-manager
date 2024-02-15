using Reddit.Things;

namespace lib;

public class CommentPreview
{

    #region Constructors

    public CommentPreview(Comment comment)
    {
        _comment = comment;
        Author = comment.Author;
        Body = comment.Body;
        Id = comment.Id;
        Name = comment.Name;
        Permalink = comment.Permalink;
        Saved = comment.Saved;
        Score = comment.Score;
        Subreddit = comment.Subreddit;
    }

    #endregion

    #region Properties

    public string Author { get; }

    public string Body { get; }

    public DateTime Date => GetValidaDate();

    public string Id { get; }

    public string Name { get; }

    public string Permalink { get; }

    public bool Saved { get; }
    public int Score { get; }
    public string Subreddit { get; }

    private readonly Comment _comment;
    
    #endregion

    #region Overridden Methods

    public override string ToString()
    {
        return $"{Id}; {Subreddit}; {Date}; {LimitString(Body)}";
    }

    #endregion

    #region Helper Methods

    private DateTime GetValidaDate()
    {
        if (_comment.CreatedUTC != DateTime.MinValue)
        {
            return _comment.CreatedUTC;
        }

        if (_comment.Created != DateTime.MinValue)
        {
            return _comment.Created;
        }

        if (_comment.Edited != DateTime.MinValue)
        {
            return _comment.Edited;
        }
        
        return DateTime.MinValue;
    }

    private static string LimitString(string input)
    {
        return input.Length > 60 ? $"{input[..57]}..." : input;
    }

    #endregion
    
}
