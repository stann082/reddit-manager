namespace lib;

public abstract class CommentBlock
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Text { get; init; } = string.Empty;
}

public class QuoteBlock : CommentBlock;

public class ResponseBlock : CommentBlock
{
    public Guid? ParentQuoteId { get; init; }
}

