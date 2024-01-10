using cli.options;
using lib;

namespace cli.commands;

public static class CommentsCommand
{

    #region Public Methods

    public static async Task<int> Execute(CommentsOptions opts, ISavedService savedService)
    {
        Console.Write("Fetching records, please wait...");
        var comments = await savedService.GetFilteredCommentsAsync(opts);
        var limitedComments = comments.Take(opts.Limit).ToArray();

        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
        Console.WriteLine();
        
        foreach (var comment in limitedComments)
        {
            Console.WriteLine($"Author:      {comment.Author}");
            Console.WriteLine($"Subreddit:   {comment.Subreddit}");
            Console.WriteLine($"Date posted: {comment.CreatedUTC}");
            Console.WriteLine($"Link:        https://reddit.com{comment.Permalink}");

            if (string.IsNullOrEmpty(opts.Query))
            {
                Console.WriteLine(comment.Body);
            }
            else
            {
                HighlightMatches(comment.Body, opts.Query);
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
