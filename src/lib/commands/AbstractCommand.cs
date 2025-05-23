using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

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

            CommentBlock[] parts = CommentParser.Parse(comment.Body);
            PrintBody(parts, options.Query);

            Console.WriteLine();
        }

        Console.WriteLine($"Showing {comments.Item1.Length} out of {comments.Item2} filtered comments");
        return await Task.FromResult(0);
    }

    #endregion

    #region Helper Methods

    private static void PrintBody(CommentBlock[] blocks, string query = null)
    {
        foreach (CommentBlock block in blocks)
        {
            if (block is QuoteBlock)
                Console.ForegroundColor = ConsoleColor.Green;

            if (!string.IsNullOrEmpty(query))
            {
                var startIndex = 0;
                var index = block.Text.IndexOf(query, StringComparison.OrdinalIgnoreCase);
                while (index != -1)
                {
                    // Print normal text before query match
                    Console.Write(block.Text.Substring(startIndex, index - startIndex));

                    // Print query in red
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(block.Text.Substring(index, query.Length));

                    // Reset color to block type
                    if (block is QuoteBlock)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ResetColor();

                    startIndex = index + query.Length;
                    index = block.Text.IndexOf(query, startIndex, StringComparison.OrdinalIgnoreCase);
                }

                // Print remaining text
                Console.Write(block.Text[startIndex..]);
            }
            else
            {
                Console.Write(block.Text);
            }

            Console.ResetColor();
            Console.Write("\n\n");
        }
    }

    #endregion

    #region Abstract Methods

    protected abstract Task<CommentModel[]> GetAllComments();
    protected abstract Task<(CommentPreview[], int)> GetFilteredComments(IOptions options);

    #endregion
}