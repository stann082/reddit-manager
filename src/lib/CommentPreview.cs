using Reddit.Things;

namespace lib;

public class CommentPreview
{

    #region Constructors

    public CommentPreview(Comment comment)
    {
        Author = comment.Author;
        Body = comment.Body;
        CreatedUTC = comment.CreatedUTC;
        Edited = comment.Edited;
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

    public DateTime CreatedUTC { get; }

    public DateTime Edited { get; }

    public string Id { get; }

    public string Name { get; }

    public string Permalink { get; }

    public bool Saved { get; }
    public int Score { get; }
    public string Subreddit { get; }

    #endregion

    #region Overridden Methods

    public override string ToString()
    {
        return $"{Id}; {Subreddit}; {CreatedUTC}; {LimitString(Body)}";
    }

    #endregion

    #region Helper Methods

    private static string LimitString(string input)
    {
        return input.Length > 60 ? $"{input[..57]}..." : input;
    }

    #endregion
    
}
