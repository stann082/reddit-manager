namespace lib;

public sealed class RedditThing<TData>
{
    public string Kind { get; set; } = string.Empty;
    public TData Data { get; set; } = default!;
}
