using CommandLine;
using lib;

namespace cli.options;

[Verb("saved", HelpText = "Command related to saved items.")]
public class SavedOptions : AbstractOptions, IOptions
{
    [Option('c', "comment", HelpText = "Specify if you're searching for comment.")]
    public bool Comment { get; set; }
    
    [Option('p', "post", HelpText = "Specify if you're searching for post.")]
    public bool Post { get; set; }
}
