using MongoDB.Bson;
using Reddit.Things;

namespace lib;

public class CommentModel
{

    #region Constructors

    // ReSharper disable once UnusedMember.Global
    public CommentModel()
    {
    }
    
    public CommentModel(Comment comment)
    {
        ApprovedAtUtc = comment.ApprovedAtUTC;
        Subreddit = comment.Subreddit;
        Saved = comment.Saved;
        ModReasonTitle = comment.ModReasonTitle;
        Gilded = comment.Gilded;
        SubredditNamePrefixed = comment.SubredditNamePrefixed;
        Downs = comment.Downs;
        Name = comment.Name;
        AuthorFlairBackgroundColor = comment.AuthorFlairBackgroundColor;
        SubredditType = comment.SubredditType;
        Ups = comment.Ups;
        AuthorFlairTemplateId = comment.AuthorFlairTemplateId;
        AuthorFullname = comment.AuthorFullname;
        CanModPost = comment.CanModPost;
        Score = comment.Score;
        ApprovedBy = comment.ApprovedBy;
        IgnoreReports = comment.IgnoreReports;
        Edited = comment.Edited;
        AuthorFlairCssClass = comment.AuthorFlairCSSClass;
        ModNote = comment.ModNote;
        Created = comment.Created;
        BannedBy = comment.BannedBy;
        AuthorFlairType = comment.AuthorFlairType;
        Likes = comment.Likes;
        BannedAtUtc = comment.BannedAtUTC;
        Archived = comment.Archived;
        NoFollow = comment.NoFollow;
        Spam = comment.Spam;
        CanGild = comment.CanGild;
        Removed = comment.Removed;
        AuthorFlairText = comment.AuthorFlairText;
        RteMode = comment.RteMode;
        Distinguished = comment.Distinguished;
        SubredditId = comment.SubredditId;
        ModReasonBy = comment.ModReasonBy;
        RemovalReason = comment.RemovalReason;
        CommentId = comment.Id;
        Author = comment.Author;
        SendReplies = comment.SendReplies;
        Approved = comment.Approved;
        AuthorFlairTextColor = comment.AuthorFlairTextColor;
        Permalink = comment.Permalink;
        Stickied = comment.Stickied;
        CreatedUtc = comment.CreatedUTC;
        BodyHtml = comment.BodyHTML;
        ParentId = comment.ParentId;
        Body = comment.Body;
        Collapsed = comment.Collapsed;
        IsSubmitter = comment.IsSubmitter;
        CollapsedReason = comment.CollapsedReason;
        ScoreHidden = comment.ScoreHidden;
        Controversiality = comment.Controversiality;
        Depth = comment.Depth;
        LinkId = comment.LinkId;
    }

    public CommentModel(PushshiftModel comment)
    {
        DateTime createdUtc = ConvertFromUnixTimestamp(comment.CreatedUtc);
        
        Subreddit = comment.Subreddit;
        Gilded = comment.Gilded;
        SubredditNamePrefixed = comment.SubredditNamePrefixed;
        Downs = comment.Score;
        AuthorFlairBackgroundColor = comment.AuthorFlairBackgroundColor;
        SubredditType = comment.SubredditType;
        Ups = comment.Score;
        AuthorFlairTemplateId = comment.AuthorFlairTemplateId;
        AuthorFullname = comment.AuthorFullname;
        CanModPost = comment.CanModPost;
        Score = comment.Score;
        Edited = createdUtc;
        AuthorFlairCssClass = comment.AuthorFlairCssClass;
        Created = createdUtc;
        AuthorFlairType = comment.AuthorFlairType;
        NoFollow = comment.NoFollow;
        CanGild = comment.CanGild;
        Removed = !string.IsNullOrEmpty(comment.RemovalReason);
        AuthorFlairText = comment.AuthorFlairText;
        Distinguished = comment.Distinguished;
        SubredditId = comment.SubredditId;
        RemovalReason = comment.RemovalReason;
        CommentId = comment.Id;
        Author = comment.Author;
        SendReplies = comment.SendReplies;
        AuthorFlairTextColor = comment.AuthorFlairTextColor;
        Permalink = comment.Permalink;
        Stickied = comment.Stickied;
        CreatedUtc = createdUtc;
        ParentId = comment.ParentId;
        Body = comment.Body;
        Collapsed = comment.Collapsed;
        IsSubmitter = comment.IsSubmitter;
        CollapsedReason = comment.CollapsedReason;
        Controversiality = comment.Controversiality;
        LinkId = comment.LinkId;
    }

    #endregion
    
    #region Properties

    public ObjectId Id { get; set; }
    public DateTime ApprovedAtUtc { get; set; }
    public string Subreddit { get; set; }
    public bool Saved { get; set; }
    public string ModReasonTitle { get; set; }
    public int Gilded { get; set; }
    public string SubredditNamePrefixed { get; set; }
    public int Downs { get; set; }
    public string Name { get; set; }
    public string AuthorFlairBackgroundColor { get; set; }
    public string SubredditType { get; set; }
    public int Ups { get; set; }
    public string AuthorFlairTemplateId { get; set; }
    public string AuthorFullname { get; set; }
    public bool CanModPost { get; set; }
    public int Score { get; set; }
    public string ApprovedBy { get; set; }
    public bool IgnoreReports { get; set; }
    public DateTime Edited { get; set; }
    public string AuthorFlairCssClass { get; set; }
    public string ModNote { get; set; }
    public DateTime Created { get; set; }
    public string BannedBy { get; set; }
    public string AuthorFlairType { get; set; }
    public bool? Likes { get; set; }
    public DateTime BannedAtUtc { get; set; }
    public bool Archived { get; set; }
    public bool NoFollow { get; set; }
    public bool Spam { get; set; }
    public bool CanGild { get; set; }
    public bool Removed { get; set; }
    public string AuthorFlairText { get; set; }
    public string RteMode { get; set; }
    public string Distinguished { get; set; }
    public string SubredditId { get; set; }
    public string ModReasonBy { get; set; }
    public string RemovalReason { get; set; }
    public string CommentId { get; set; }
    public string Author { get; set; }
    public bool SendReplies { get; set; }
    public bool Approved { get; set; }
    public string AuthorFlairTextColor { get; set; }
    public string Permalink { get; set; }
    public bool Stickied { get; set; }
    public DateTime CreatedUtc { get; set; }
    public string BodyHtml { get; set; }
    public string ParentId { get; set; }
    public string Body { get; set; }
    public bool Collapsed { get; set; }
    public bool IsSubmitter { get; set; }
    public string CollapsedReason { get; set; }
    public bool ScoreHidden { get; set; }
    public int Controversiality { get; set; }
    public int Depth { get; set; }
    public string LinkId { get; set; }

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