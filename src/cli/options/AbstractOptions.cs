using CommandLine;

namespace cli.options;

public abstract class AbstractOptions
{
    
    [Option('c', "comment", HelpText = "Specify if you're searching for comment.")]
    public bool Comment { get; set; }
    
    [Option('f', "filter", HelpText = "Filters by sub, author, date (e.g., -f author=foomanchu&sub=news.")]
    public string Filter { get; set; }
    
    [Option('l', "limit", Default = 10, HelpText = "Number of items to display.")]
    public int Limit { get; set; }

    [Option('p', "post", HelpText = "Specify if you're searching for post.")]
    public bool Post { get; set; }
    
    [Option('q', "query", HelpText = "Search for a specific word.")]
    public string Query { get; set; }
    
}
