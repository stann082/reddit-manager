using lib;
using Reddit.Things;

namespace cli.commands;

public abstract class AbstractCommand
{

    #region Constructors

    protected AbstractCommand(IOptions options)
    {
        _options = options;
    }

    #endregion

    #region Variables

    private readonly IOptions _options;

    #endregion

    #region Abstract Methods

    protected abstract Task<Comment[]> GetComments(IOptions options);

    #endregion
    
    #region Public Methods

    public async Task<int> Execute()
    {
        Console.Write("Fetching records, please wait...");
        var comments = await GetComments(_options);
        var limitedComments = comments.Take(_options.Limit).ToArray();

        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
        Console.WriteLine();
        
        foreach (var comment in limitedComments)
        {
            Console.WriteLine($"Author:      {comment.Author}");
            Console.WriteLine($"Subreddit:   {comment.Subreddit}");
            Console.WriteLine($"Date posted: {comment.CreatedUTC}");
            Console.WriteLine($"Score:       {comment.Score}");
            Console.WriteLine($"Link:        https://old.reddit.com{comment.Permalink}");

            if (string.IsNullOrEmpty(_options.Query))
            {
                Console.WriteLine(comment.Body);
            }
            else
            {
                HighlightMatches(comment.Body, _options.Query);
            }

            Console.WriteLine();
        }

        Console.WriteLine($"Showing {limitedComments.Length} out of {comments.Length} comments");
        return await Task.FromResult(0);
    }

    #endregion

    #region Helper Methods

    private static void HighlightMatches(string text, string query)
    {
        var queryLength = query.Length;
        int startIndex = 0;
        int index = text.IndexOf(query, StringComparison.OrdinalIgnoreCase);

        while (index != -1)
        {
            Console.Write(text.Substring(startIndex, index - startIndex));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text.Substring(index, queryLength));
            Console.ResetColor();

            startIndex = index + queryLength;
            index = text.IndexOf(query, startIndex, StringComparison.OrdinalIgnoreCase);
        }

        Console.Write(text[startIndex..]);
        Console.WriteLine();
    }

    #endregion

}
