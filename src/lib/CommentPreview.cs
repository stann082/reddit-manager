namespace lib;

public class CommentPreview
{
    #region Overridden Methods

    public override string ToString()
    {
        return $"{Id}; {Subreddit}; {Date}; {LimitString(Body)}";
    }

    #endregion

    #region Constructors

    public CommentPreview(CommentModel comment)
    {
        Author = comment.Author;
        Body = comment.Body;
        Id = comment.CommentId;
        Date = GetValidaDate(comment);
        Name = comment.Name;
        Permalink = comment.Permalink;
        Saved = comment.Saved;
        Score = comment.Score;
        Subreddit = comment.Subreddit;
    }

    public CommentPreview(PushshiftModel model)
    {
        Author = model.Author;
        Body = model.Body;
        Id = model.Id;
        Date = ConvertFromUnixTimestamp(model.CreatedUtc);
        Permalink = model.Permalink;
        Score = model.Score;
        Subreddit = model.Subreddit;
    }

    #endregion

    #region Properties

    public string Author { get; }

    public string Body { get; }

    public DateTime? Date { get; }

    public string Id { get; }

    public string Name { get; }

    public string Permalink { get; }

    public bool Saved { get; }
    public int Score { get; }
    public string Subreddit { get; }

    #endregion

    #region Helper Methods

    private static DateTime ConvertFromUnixTimestamp(long unixTimeStamp)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }

    private DateTime GetValidaDate(CommentModel comment)
    {
        if (comment.CreatedUtc != DateTime.MinValue) return comment.CreatedUtc;

#pragma warning disable CS0618 // Type or member is obsolete
        if (comment?.Created != DateTime.MinValue) return comment.Created;
#pragma warning restore CS0618 // Type or member is obsolete

        if (comment.Edited != DateTime.MinValue) return comment.Edited;
        return DateTime.MinValue;
    }

    private static string LimitString(string input)
    {
        return input.Length > 60 ? $"{input[..57]}..." : input;
    }

    #endregion
}