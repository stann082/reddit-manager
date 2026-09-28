using CommandLine;

namespace lib.options;

[Verb("search", HelpText = "Search reddit.")]
public class SearchOptions : AbstractOptions, IOptions
{
    [Option("thread-id", HelpText = "Fetch all comments from a specific thread by ID.")]
    public string ThreadIdValue { get; set; }

    public override string ThreadId => ThreadIdValue;
}