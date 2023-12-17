using CommandLine;
using lib;

namespace cli.options;

[Verb("comments", HelpText = "Comment commands.")]
public class CommentOptions : ICommentOptions
{

    [Option("cache-saved-comments", HelpText = "Caches saved comments.")]
    public bool CacheSavedComments { get; set; }

    [Option('q', "query", HelpText = "Search for a specific word.")]
    public string Query { get; set; }

    [Option('f', "filter", HelpText = "Filters by sub, author, date (e.g.: -f author=foomanchu&sub=news.")]
    public string Filter { get; set; }
    
    [Option('l', "limit", Default = 10, HelpText = "Number of comments to display.")]
    public int Limit { get; set; }

}
