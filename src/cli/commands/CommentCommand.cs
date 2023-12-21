using cli.options;
using lib;

namespace cli.commands;

public static class CommentCommand
{

    #region Public Methods

    public static async Task<int> Execute(CommentOptions opts, IRedditService redditService)
    {
        if (opts.CacheSavedComments)
        {
            await redditService.CacheSavedCommentsAsync();
            return await Task.FromResult(0);
        }

        var cachedComments = await redditService.GetFilteredCommentsAsync(opts);
        var limitedComments = cachedComments.Take(opts.Limit).ToArray();

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

        Console.WriteLine($"Showing {limitedComments.Length} out of {cachedComments.Length} comments");
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
