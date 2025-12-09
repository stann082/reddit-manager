using System;

namespace lib;

public class CommentPreview(CommentModel comment)
{

    #region Properties

    public string Author { get; } = comment.Author;

    public string Body { get; } = comment.Body;

    public DateTime? Date { get; } = GetValidaDate(comment);

    public string Id { get; } = comment.CommentId;
    
    public bool IsArchive { get; } = comment.IsArchive;

    public CommentBlock[] ParsedBlocks => !string.IsNullOrWhiteSpace(Body) ? CommentParser.Parse(Body) : [];
    public string Permalink { get; } = comment.Permalink;

    public int Score { get; } = comment.Score;
    public string Subreddit { get; } = comment.Subreddit;

    #endregion

    #region Overridden Methods

    public override string ToString()
    {
        return $"{Id}; {Subreddit}; {Date}; {LimitString(Body)}";
    }

    #endregion
    
    #region Helper Methods

    private static DateTime GetValidaDate(CommentModel comment)
    {
        if (comment.CreatedUtc != DateTime.MinValue) return comment.CreatedUtc;
        if (comment.Created != DateTime.MinValue) return comment.Created;
        return comment.Edited != DateTime.MinValue ? comment.Edited : DateTime.MinValue;
    }

    private static string LimitString(string input)
    {
        return input.Length > 60 ? $"{input[..57]}..." : input;
    }

    #endregion
    
}