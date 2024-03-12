using CommandLine;
using lib;

namespace cli;

[Verb("saved", HelpText = "Command related to saved items.")]
public class SavedOptions : AbstractOptions, IOptions
{
    
}
