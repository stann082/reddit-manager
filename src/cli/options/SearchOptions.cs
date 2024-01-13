using CommandLine;
using lib;

namespace cli.options;

[Verb("search", HelpText = "Search reddit.")]
public class SearchOptions : AbstractOptions, ISearchOptions
{
    [Option('c', "comment", HelpText = "Specify if you're searching for comment.")]
    public bool Comment { get; set; }
    
    [Option('p', "post", HelpText = "Specify if you're searching for post.")]
    public bool Post { get; set; }
}
