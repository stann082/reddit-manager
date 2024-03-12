using CommandLine;
using lib.options;

namespace cli;

[Verb("saved", HelpText = "Command related to saved items.")]
public class SavedOptions : AbstractOptions, IOptions
{

}
