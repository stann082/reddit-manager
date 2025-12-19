using System;
using MongoDB.Bson.Serialization.Attributes;

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

    public CommentModel(RedditComment comment)
    {
        Author = comment.Author;
        Body = comment.Body;
        BodyHtml = comment.Body_Html;
        CommentId = comment.Id;
        Controversiality = comment.Controversiality ?? 0;
        Created = DateTimeOffset.FromUnixTimeSeconds((long)(comment.Created ?? 0)).LocalDateTime;
        CreatedUtc = DateTimeOffset.FromUnixTimeSeconds((long)(comment.Created_Utc ?? 0)).UtcDateTime;
        Edited = DateTime.MinValue;
        LinkId = comment.Link_Id;
        Name = comment.Name;
        NoFollow = comment.No_Follow ?? true;
        ParentId = comment.Parent_Id;
        Permalink = comment.Permalink;
        Removed = !string.IsNullOrEmpty(comment.Removal_Reason);
        Score = comment.Score ?? 0;
        Subreddit = comment.Subreddit;
        SubredditId = comment.Subreddit_Id;
        SubredditType = comment.Subreddit_Type;
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

    [BsonId]
    public string CommentId { get; set; }

    public string Author { get; set; }
    public string Body { get; set; }
    public string BodyHtml { get; set; }
    public int Controversiality { get; set; }
    public DateTime Created { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime Edited { get; set; }
    public bool IsArchive { get; set; }
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
