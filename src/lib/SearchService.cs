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
    /// </summary>
    public async Task<List<CommentModel>> GetThreadCommentsAsync(string threadId)
    {
        try
        {
            // Fetch the thread - returns array: [thread_info, comments_listing]
            var json = await redditClient.GetJsonAsync($"comments/{threadId}.json");
            var threadData = JsonSerializer.Deserialize<List<RedditListing<RedditThing<JsonElement>>>>(json, JsonOpts);
            
            var comments = new List<CommentModel>();
            
            if (threadData?.Count >= 2)
            {
                // Second element contains the comments listing
                var commentListing = threadData[1];
                await ProcessCommentsAsync(commentListing.Data.Children, comments);
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

    /// <summary>
    /// Recursively process comments from a Reddit listing, including nested replies.
    /// Handles both regular comments (t1) and more_comments tokens.
    /// </summary>
    private async Task ProcessCommentsAsync(List<RedditThing<JsonElement>> children, List<CommentModel> allComments)
    {
        foreach (var child in children)
        {
            // t1 = Comment
            if (child.Kind == "t1")
            {
                var comment = child.Data.Deserialize<RedditComment>(JsonOpts);
                if (comment != null)
                {
                    allComments.Add(new CommentModel(comment));
                    
                    // Recursively process nested replies if they exist
                    if (!comment.Replies.Equals(default(JsonElement)) && comment.Replies.ValueKind != System.Text.Json.JsonValueKind.Null)
                    {
                        try
                        {
                            var repliesListing = comment.Replies.Deserialize<RedditListing<RedditThing<JsonElement>>>(JsonOpts);
                            if (repliesListing?.Data?.Children.Count > 0)
                            {
                                await ProcessCommentsAsync(repliesListing.Data.Children, allComments);
                            }
                        }
                        catch
                        {
                            // Silently ignore if replies can't be deserialized
                        }
                    }
                }
            }
            // more = MoreComments token (additional comments not yet loaded)
            else if (child.Kind == "more")
            {
                // Optional enhancement: Implement loading of "more_comments" by calling /api/info
                // This would require making additional API requests with the comment IDs
                // For now, we skip more_comments tokens
            }
        }
    }

    #endregion
}
