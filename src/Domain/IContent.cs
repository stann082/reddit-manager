namespace Domain
{
    public interface IContent
    {

        string Author { get; }

        long CreatedUtc { get; }

        string Permalink { get; }

        int Score { get; }
        string Subreddit { get; }

        string Text { get; }

        long? UpdatedUtc { get; }

    }
}
