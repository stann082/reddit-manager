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
/// API Details:
/// - Endpoint: https://oauth.reddit.com/search (or /r/{subreddit}/search)
/// - Requires OAuth2 Bearer token authentication
/// - Returns paginated results with cursor-based pagination (after token)
/// - No total count provided by API
/// 
/// Search Parameters:
/// - query (q): Reddit search syntax (supports author:username for author restrictions)
/// - sort: relevance, hot, top, new, comments
/// - time (t): all, day, hour, month, week, year
/// - type: comment (to filter for comments only)
/// - limit: number of results per page (1-100)
/// - after: cursor token for next page of results
/// - restrict_sr: whether to restrict search to a specific subreddit
/// </summary>
public class SearchService(IRedditClient redditClient) : AbstractService, ISearchService
{
    
    #region Constants/Static Fields

    private const int MaxCommentTreeLimit = 500;
    private const int MaxMoreChildrenPerRequest = 100;

    private static readonly JsonSerializerOptions JsonOpts = new() { PropertyNameCaseInsensitive = true };

    #endregion

    #region Public Methods

    public async Task<(CommentPreview[] Comments, string NextAfter)> SearchCommentsAsync(IOptions options)
    {
        try
        {
            // Build Reddit query syntax:
            // - author restriction is typically done as "author:NAME"
            // - optionally restrict to a subreddit by calling /r/{sub}/search or using restrict_sr=on
            var q = options.Query ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(options.Author))
            {
                q = $"author:{options.Author} {q}".Trim();
            }

            string sort = options.Sort;
            string time = options.Time;
            string after = options.After;
            var qs = new List<string>
            {
                $"q={Uri.EscapeDataString(q)}",
                "type=comment",
                $"limit={options.Limit}",
                $"sort={Uri.EscapeDataString(sort)}",
                $"t={Uri.EscapeDataString(time)}",
                "restrict_sr=false",
                "raw_json=1"
            };

            if (!string.IsNullOrWhiteSpace(after))
            {
                qs.Add($"after={Uri.EscapeDataString(after)}");
            }

            // If you want to restrict to a subreddit, use /r/{sub}/search and restrict_sr=on
            string url = string.IsNullOrWhiteSpace(options.Subreddit)
                ? $"https://oauth.reddit.com/search?{string.Join("&", qs)}"
                : $"https://oauth.reddit.com/r/{Uri.EscapeDataString(options.Subreddit)}/search?{string.Join("&", qs).Replace("restrict_sr=false", "restrict_sr=on")}";

            var json = await redditClient.GetJsonAsync(url);

            var listing = JsonSerializer.Deserialize<RedditListing<RedditThing<JsonElement>>>(json, JsonOpts);
            var children = listing?.Data?.Children;
            if (children is null || children.Count == 0)
            {
                return ([], null);
            }

            var results = new List<CommentModel>(children.Count);

            foreach (var child in children)
            {
                if (!string.Equals(child.Kind, "t1", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var c = child.Data.Deserialize<RedditComment>(JsonOpts);
                if (c is null)
                {
                    continue;
                }

                results.Add(new CommentModel(c));
            }

            return (results.Select(r => new CommentPreview(r)).ToArray(), listing!.Data.After);
        }
        catch (Exception ex)
        {
            LoggingManager.LogException(ex);
            return ([], null);
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
