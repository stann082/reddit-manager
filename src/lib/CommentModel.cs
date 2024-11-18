using MongoDB.Bson;
using Reddit.Things;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable once UnusedMember.Global
#pragma warning disable CS0618 // Type or member is obsolete

namespace lib;

public class CommentModel
{

    #region Constructors

    public CommentModel()
    {
    }

    public CommentModel(CommentModel comment)
    {
        Author = comment.Author;
        Body = comment.Body;
        BodyHtml = comment.BodyHtml;
        CommentId = comment.CommentId;
        Controversiality = comment.Controversiality;
        Created = comment.Created;
        CreatedUtc = comment.CreatedUtc;
        Edited = comment.Edited;
        Id = comment.Id;
        LinkId = comment.LinkId;
        Name = comment.Name;
        NoFollow = comment.NoFollow;
        ParentId = comment.ParentId;
        Permalink = comment.Permalink;
        Removed = comment.Removed;
        Score = comment.Score;
        Subreddit = comment.Subreddit;
        SubredditId = comment.SubredditId;
        SubredditType = comment.SubredditType;
    }

    public CommentModel(Comment comment)
    {
        Author = comment.Author;
        Body = comment.Body;
        BodyHtml = comment.BodyHTML;
        CommentId = comment.Id;
        Controversiality = comment.Controversiality;
        Created = comment.Created;
        CreatedUtc = comment.CreatedUTC;
        Edited = comment.Edited;
        LinkId = comment.LinkId;
        Name = comment.Name;
        NoFollow = comment.NoFollow;
        ParentId = comment.ParentId;
        Permalink = comment.Permalink;
        Removed = comment.Removed;
        Score = comment.Score;
        Subreddit = comment.Subreddit;
        SubredditId = comment.SubredditId;
        SubredditType = comment.SubredditType;
    }

    public CommentModel(PushshiftModel comment)
    {
        var createdUtc = ConvertFromUnixTimestamp(comment.CreatedUtc);

        Author = comment.Author;
        Body = comment.Body;
        CommentId = comment.Id;
        Controversiality = comment.Controversiality;
        Created = createdUtc;
        CreatedUtc = createdUtc;
        Edited = comment.Edited ? createdUtc.AddMinutes(10) : DateTime.MinValue;
        LinkId = comment.LinkId;
        Name = $"t1_{comment.Id}";
        NoFollow = comment.NoFollow;
        ParentId = comment.ParentId;
        Permalink = comment.Permalink;
        Removed = !string.IsNullOrEmpty(comment.RemovalReason);
        Score = comment.Score;
        Subreddit = comment.Subreddit;
        SubredditId = comment.SubredditId;
        SubredditType = comment.SubredditType;
    }

    #endregion

    #region Properties

    public string Author { get; set; }
    public string Body { get; set; }
    public string BodyHtml { get; set; }
    public string CommentId { get; set; }
    public int Controversiality { get; set; }
    public DateTime Created { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime Edited { get; set; }
    public ObjectId Id { get; set; }
    public string LinkId { get; set; }
    public string Name { get; set; }
    public bool NoFollow { get; set; }
    public string ParentId { get; set; }
    public string Permalink { get; set; }
    public bool Removed { get; set; }
    public int Score { get; set; }
    public string Subreddit { get; set; }
    public string SubredditId { get; set; }
    public string SubredditType { get; set; }

    #endregion
    
    #region Helper Methods

    private static DateTime ConvertFromUnixTimestamp(long unixTimeStamp)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }

    #endregion
}