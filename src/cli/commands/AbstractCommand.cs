using System.Text.Json;
using lib;
using Reddit.Things;

namespace cli.commands;

public abstract class AbstractCommand(IOptions options)
{
    
    #region Public Methods

    public async Task<int> Execute()
    {
        if (options.ShouldExport)
        {
            Comment[] allComments = await GetAllComments();
            string jsonString = JsonSerializer.Serialize(allComments);
            string filePath = @"C:\Users\sbennett\reddit-backup.json";
            await File.WriteAllTextAsync(filePath, jsonString);

            jsonString = await File.ReadAllTextAsync(filePath);
            Comment[] deserializedComments = JsonSerializer.Deserialize<Comment[]>(jsonString);
            return 0;
        }

        Console.Write("Fetching records, please wait...");
        var comments = await GetFilteredComments(options);
        var limitedComments = comments.Take(options.Limit).ToArray();

        Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
        Console.WriteLine();

        foreach (var comment in limitedComments)
        {
            if (options.ShowId)
            {
                Console.WriteLine($"Id:          {comment.Id}");
            }

            Console.WriteLine($"Author:      {comment.Author}");
            Console.WriteLine($"Subreddit:   {comment.Subreddit}");
            Console.WriteLine($"Date posted: {comment.Date}");
            Console.WriteLine($"Score:       {comment.Score}");
            Console.WriteLine($"Link:        https://old.reddit.com{comment.Permalink}");
            HighlightText(comment.Body, options.Query);

            Console.WriteLine();
        }

        Console.WriteLine($"Showing {limitedComments.Length} out of {comments.Length} comments");
        return await Task.FromResult(0);
    }

    #endregion

    #region Abstract Methods

    protected abstract Task<Comment[]> GetAllComments();
    protected abstract Task<CommentPreview[]> GetFilteredComments(IOptions options);

    #endregion

    #region Helper Methods

    private static void HighlightText(string text, string query = null)
    {
        var parts = text.Split(["\n\n"], StringSplitOptions.None);
        foreach (var originalPart in parts)
        {
            string part = originalPart.StartsWith("&gt;") ? originalPart.Replace("&gt;", ">") : originalPart;
            bool isQuote = originalPart.StartsWith("&gt;");
            if (isQuote)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            if (!string.IsNullOrEmpty(query))
            {
                int startIndex = 0;
                int index = part.IndexOf(query, StringComparison.OrdinalIgnoreCase);
                while (index != -1)
                {
                    Console.Write(part.Substring(startIndex, index - startIndex));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(part.Substring(index, query.Length));
                    if (isQuote)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ResetColor();
                    }

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
    
}