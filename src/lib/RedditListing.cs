using System.Collections.Generic;

namespace lib;

public sealed class RedditListing<TChild>
{
    public RedditListingData<TChild> Data { get; set; } = new();
}

public sealed class RedditListingData<TChild>
{
    public string After { get; set; }
    public List<TChild> Children { get; set; } = new();
}

