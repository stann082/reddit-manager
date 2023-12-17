using System.Text;
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
        foreach (var comment in cachedComments)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Author:      {comment.Author}");
            sb.AppendLine($"Subreddit:   {comment.Subreddit}");
            sb.AppendLine($"Date posted: {comment.CreatedUTC}");
            sb.AppendLine($"Link:        https://reddit.com{comment.Permalink}");
            sb.AppendLine(comment.Body);
            Console.WriteLine(sb.ToString());
        }

        Console.WriteLine($"Total comments: {cachedComments.Length}");
        return await Task.FromResult(0);
    }

    #endregion

}
