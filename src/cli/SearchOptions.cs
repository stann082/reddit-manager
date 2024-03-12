using CommandLine;
using lib.options;

namespace cli;

[Verb("search", HelpText = "Search reddit.")]
public class SearchOptions : AbstractOptions, IOptions
{
}
