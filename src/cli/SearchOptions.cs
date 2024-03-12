using CommandLine;
using lib;

namespace cli;

[Verb("search", HelpText = "Search reddit.")]
public class SearchOptions : AbstractOptions, IOptions
{
}
