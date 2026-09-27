using System.Collections.Generic;
using System.Text.Json;

namespace lib;

/// <summary>
/// A "more" stub in a comment tree, representing comments that were not included in the response.
/// An empty Children list means a "continue this thread" link (the subtree is too deep).
/// </summary>
public sealed class RedditMore
{
    public int Count { get; set; }
    public string Id { get; set; }
    public string Parent_Id { get; set; }
    public List<string> Children { get; set; } = new();
}

/// <summary>
/// Response shape of GET /api/morechildren?api_type=json.
/// </summary>
public sealed class RedditMoreChildrenResponse
{
    public RedditMoreChildrenJson Json { get; set; }
}

public sealed class RedditMoreChildrenJson
{
    public RedditMoreChildrenData Data { get; set; }
}

public sealed class RedditMoreChildrenData
{
    public List<RedditThing<JsonElement>> Things { get; set; } = new();
}
