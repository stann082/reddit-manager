using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace lib;

/// <summary>
/// Service for searching Reddit comments using the official Reddit OAuth API.
/// This is part of the custom Reddit library implementation that replaced third-party dependencies.
/// 
/// Comment search:
/// Reddit's /search endpoint only returns posts (type=comment is ignored), so comments are searched
/// by paging through a user's comment history (GET /user/{username}/comments) and filtering locally.
/// The author defaults to the logged-in user. Reddit caps listings at ~1000 items, so only the
/// most recent ~1000 comments of a user are searchable this way.
/// </summary>
public class SearchService(IRedditClient redditClient) : AbstractService, ISearchService
{
    
    #region Constants/Static Fields

    private const int MaxCommentTreeLimit = 500;
    private const int MaxMoreChildrenPerRequest = 100;
    private const int UserCommentsPageLimit = 100;

    private static readonly JsonSerializerOptions JsonOpts = new() { PropertyNameCaseInsensitive = true };

    #endregion

    #region Public Methods

    public async Task<(CommentPreview[] Comments, int Total)> SearchCommentsAsync(IOptions options)
    {
        try
        {
            string username = !string.IsNullOrWhiteSpace(options.Author)
                ? options.Author
                : await redditClient.LogIn();

            var comments = (await GetUserCommentsAsync(username))
                .Select(c => new CommentPreview(c))
                .OrderByDescending(c => c.Date)
                .ToArray();

            var filteredComments = FilterComments(comments, options).ToArray();
            return (filteredComments.Take(options.Limit).ToArray(), filteredComments.Length);
        }
        catch (Exception ex)
        {
            LoggingManager.LogException(ex);
            return ([], 0);
        }
    }

    /// <summary>
    /// Fetch all comments from a Reddit thread by thread ID.
    /// 
    /// API Details:
    /// - Endpoint: GET /comments/{threadId}.json
    /// - Returns a listing array with thread info and comments
    /// - Comments are nested in a tree structure with replies
    /// - Top-level comments are in the second element of the response array
    /// - Only a partial tree is returned (max 500); the rest is represented by "more" stubs,
    ///   which are expanded via GET /api/morechildren (max 100 IDs per request)
    /// </summary>
    public async Task<List<CommentModel>> GetThreadCommentsAsync(string threadId)
    {
        try
        {
            var comments = new List<CommentModel>();
            var seenIds = new HashSet<string>();
            var pending = new Queue<RedditMore>();

            // Fetch the thread - returns array: [thread_info, comments_listing]
            await FetchCommentTreeAsync($"comments/{threadId}.json?limit={MaxCommentTreeLimit}&raw_json=1", comments, seenIds, pending);

            while (pending.Count > 0)
            {
                var more = pending.Dequeue();

                if (more.Children is { Count: > 0 })
                {
                    // Expand the stub in batches of up to 100 IDs
                    var ids = more.Children.Where(id => !seenIds.Contains(id)).ToList();
                    for (int i = 0; i < ids.Count; i += MaxMoreChildrenPerRequest)
                    {
                        var batch = ids.Skip(i).Take(MaxMoreChildrenPerRequest);
                        var url = $"api/morechildren?api_type=json&link_id=t3_{threadId}&children={string.Join(",", batch)}&limit_children=false&raw_json=1";
                        var json = await redditClient.GetJsonAsync(url);
                        var response = JsonSerializer.Deserialize<RedditMoreChildrenResponse>(json, JsonOpts);
                        ProcessComments(response?.Json?.Data?.Things ?? [], comments, seenIds, pending);
                    }
                }
                else if (!string.IsNullOrEmpty(more.Parent_Id) && more.Parent_Id.StartsWith("t1_"))
                {
                    // "Continue this thread" stub (no child IDs): re-fetch the thread focused on the parent comment
                    var parentId = more.Parent_Id[3..];
                    await FetchCommentTreeAsync($"comments/{threadId}.json?comment={parentId}&limit={MaxCommentTreeLimit}&raw_json=1", comments, seenIds, pending);
                }
            }

            return comments;
        }
        catch (Exception ex)
        {
            LoggingManager.LogException(ex);
            throw new Exception($"Failed to fetch thread comments for thread {threadId}: {ex.Message}", ex);
        }
    }

    #endregion

    #region Private Methods

    private async Task<List<CommentModel>> GetUserCommentsAsync(string username)
    {
        var comments = new List<CommentModel>();
        string after = null;

        do
        {
            var url = $"user/{Uri.EscapeDataString(username)}/comments?limit={UserCommentsPageLimit}&sort=new&raw_json=1";
            if (!string.IsNullOrWhiteSpace(after))
            {
                url += $"&after={Uri.EscapeDataString(after)}";
            }

            var json = await redditClient.GetJsonAsync(url);
            var listing = JsonSerializer.Deserialize<RedditListing<RedditThing<JsonElement>>>(json, JsonOpts);
            var children = listing?.Data?.Children;
            if (children is null || children.Count == 0)
            {
                break;
            }

            comments.AddRange(from child in children
                where string.Equals(child.Kind, "t1", StringComparison.OrdinalIgnoreCase)
                select child.Data.Deserialize<RedditComment>(JsonOpts)
                into comment
                where comment is not null
                select new CommentModel(comment));

            after = listing.Data.After;
        } while (!string.IsNullOrWhiteSpace(after));

        return comments;
    }

    private async Task FetchCommentTreeAsync(string url, List<CommentModel> allComments, HashSet<string> seenIds, Queue<RedditMore> pending)
    {
        var json = await redditClient.GetJsonAsync(url);
        var threadData = JsonSerializer.Deserialize<List<RedditListing<RedditThing<JsonElement>>>>(json, JsonOpts);

        if (threadData?.Count >= 2)
        {
            // Second element contains the comments listing
            ProcessComments(threadData[1].Data.Children, allComments, seenIds, pending);
        }
    }

    /// <summary>
    /// Recursively process comments from a Reddit listing, including nested replies.
    /// Regular comments (t1) are collected; "more" stubs are queued for later expansion.
    /// </summary>
    private static void ProcessComments(List<RedditThing<JsonElement>> children, List<CommentModel> allComments, HashSet<string> seenIds, Queue<RedditMore> pending)
    {
        foreach (var child in children)
        {
            // t1 = Comment
            if (child.Kind == "t1")
            {
                var comment = child.Data.Deserialize<RedditComment>(JsonOpts);
                if (comment == null)
                {
                    continue;
                }

                if (seenIds.Add(comment.Id))
                {
                    allComments.Add(new CommentModel(comment));
                }

                // Recursively process nested replies if they exist (Reddit sends "" when there are none)
                if (comment.Replies.ValueKind == JsonValueKind.Object)
                {
                    var repliesListing = comment.Replies.Deserialize<RedditListing<RedditThing<JsonElement>>>(JsonOpts);
                    if (repliesListing?.Data?.Children.Count > 0)
                    {
                        ProcessComments(repliesListing.Data.Children, allComments, seenIds, pending);
                    }
                }
            }
            // more = MoreComments token (additional comments not yet loaded)
            else if (child.Kind == "more")
            {
                var more = child.Data.Deserialize<RedditMore>(JsonOpts);
                if (more != null)
                {
                    pending.Enqueue(more);
                }
            }
        }
    }

    #endregion
}
