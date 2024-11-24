using CommandLine;

namespace lib.options;

[Verb("search", HelpText = "Search reddit.")]
public class SearchOptions : AbstractOptions, IOptions
{
}