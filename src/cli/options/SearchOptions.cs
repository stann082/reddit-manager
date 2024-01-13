using CommandLine;
using lib;

namespace cli.options;

[Verb("search", HelpText = "Search reddit.")]
public class SearchOptions : AbstractOptions, IOptions
{
}
