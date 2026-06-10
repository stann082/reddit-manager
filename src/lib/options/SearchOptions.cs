using CommandLine;

namespace lib.options;

[Verb("search", HelpText = "Search reddit.")]
public class SearchOptions : AbstractOptions, IOptions
{
    [Option('s', "sort", HelpText = "Sort results by: relevance (default), hot, top, new, or comments.")]
    public string SortValue { get; set; } = "relevance";

    [Option('t', "time", HelpText = "Time filter: all (default), day, hour, month, week, or year.")]
    public string TimeValue { get; set; } = "all";

    [Option("after", HelpText = "Pagination token for fetching next page of results.")]
    public string AfterValue { get; set; }

    public override string Sort => SortValue;
    public override string Time => TimeValue;
    public override string After => AfterValue;
}