namespace Domain
{
    public interface IContent
    {

        string Author { get; }

        long CreatedUtc { get; }

        string Permalink { get; }

        int Score { get; }
        string Subreddit { get; }

        string Message { get; }

        string Quote { get; }

        long? UpdatedUtc { get; }

    }
}
