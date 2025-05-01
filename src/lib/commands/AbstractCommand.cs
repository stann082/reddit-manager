using System.Text.Json;

namespace lib.commands;

public abstract class AbstractCommand(IOptions options)
{
    #region Public Methods

    public async Task<int> Execute()
    {
        if (options.ShouldExport)
        {
            var allComments = await GetAllComments();
            var jsonString = JsonSerializer.Serialize(allComments);
            await File.WriteAllTextAsync(@"C:\Users\sbennett\reddit-backup.json", jsonString);
            return 0;
        }

        Console.Write("Fetching records, please wait...");
        var comments = await GetFilteredComments(options);

        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
        Console.WriteLine();

        foreach (var comment in comments.Item1)
        {
            if (options.ShowId) Console.WriteLine($"Id:          {comment.Id}");

            Console.WriteLine($"Author:      {comment.Author}");
            Console.WriteLine($"Subreddit:   {comment.Subreddit}");
            Console.WriteLine($"Date posted: {comment.Date}");
            Console.WriteLine($"Score:       {comment.Score}");
            
            string urlPrefix = comment.Body == "[removed]" ? "https://undelete.pullpush.io" : "https://old.reddit.com";
            Console.WriteLine($"Link:        {urlPrefix}{comment.Permalink}");
            PrintBody(comment.Body, options.Query);

            Console.WriteLine();
        }

        Console.WriteLine($"Showing {comments.Item1.Length} out of {comments.Item2} filtered comments");
        return await Task.FromResult(0);
    }

    #endregion

    #region Helper Methods

    private static void PrintBody(string text, string query = null)
    {
        var parts = text.Split(["\n\n"], StringSplitOptions.None);
        foreach (var originalPart in parts)
        {
            var part = originalPart.StartsWith("&gt;") ? originalPart.Replace("&gt;", ">") : originalPart;
            var isQuote = originalPart.StartsWith("&gt;");
            if (isQuote) Console.ForegroundColor = ConsoleColor.Green;

            if (!string.IsNullOrEmpty(query))
            {
                var startIndex = 0;
                var index = part.IndexOf(query, StringComparison.OrdinalIgnoreCase);
                while (index != -1)
                {
                    Console.Write(part.Substring(startIndex, index - startIndex));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(part.Substring(index, query.Length));
                    if (isQuote)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ResetColor();

                    startIndex = index + query.Length;
                    index = part.IndexOf(query, startIndex, StringComparison.OrdinalIgnoreCase);
                }

                Console.Write(part[startIndex..]);
            }
            else
            {
                Console.Write(part);
            }

            if (isQuote) Console.ResetColor();
            Console.Write("\n\n");
        }
    }

    #endregion

    #region Abstract Methods

    protected abstract Task<CommentModel[]> GetAllComments();
    protected abstract Task<(CommentPreview[], int)> GetFilteredComments(IOptions options);

    #endregion
}