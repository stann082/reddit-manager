using CommandLine;

namespace cli.options;

public abstract class AbstractOptions
{
    
    [Option('f', "filter", HelpText = "Filters by sub, author, date (e.g., -f author=foomanchu&sub=news.")]
    public string Filter { get; set; }
    
    [Option('l', "limit", Default = 10, HelpText = "Number of items to display.")]
    public int Limit { get; set; }

    [Option('q', "query", HelpText = "Search for a specific word.")]
    public string Query { get; set; }
    
    [Option('u', "user", HelpText = "Specify a user (if blank your personal account will be used).")]
    public string User { get; set; }
}
