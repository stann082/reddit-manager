using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace lib.options;

public abstract class AbstractOptions
{
    
    #region Constants

    private const int DefaultLimit = 25;

    #endregion

    #region Parameters

    [Option('c', "comment", Default = true, HelpText = "Specify if you're searching for comment.")]
    public bool Comment { get; set; }

    [Option('f', "filter", HelpText = "Filters by sub, author, date (e.g., -f author=foomanchu&sub=news.")]
    public string Filter { get; set; }

    [Option('a', "archive", HelpText = "Search in Pushshift file dumps on disk.")]
    public bool IsArchive { get; set; }

    [Option('e', "exact", HelpText = "Specify if you're searching for exact word in a query.")]
    public bool IsExactWord { get; set; }

    [Option('q', "query", HelpText = "Search for a specific word.")]
    public string Query { get; set; }

    [Option("export", HelpText = "Export all saved posts to JSON.")]
    public bool ShouldExport { get; set; }

    [Option("show-id", HelpText = "Display comment id.")]
    public bool ShowId { get; set; }

    #endregion

    #region Properties

    public string Author => GetFilterValue("author");

    public string Id => GetFilterValue("id");
    public bool IsDescending => GetFilterValue("order") == "desc";
    public bool IsFilterEnabled => !string.IsNullOrEmpty(Filter);

    public int Limit => GetLimitFromFilter();

    public bool SortByDate => GetFilterValue("sort_by") == "date";
    public bool SortByScore => GetFilterValue("sort_by") == "score";
    public DateTime StartDate => ParseDate("start", DateTime.MinValue);
    public DateTime StopDate => ParseDate("stop", DateTime.MaxValue);
    public string Subreddit => GetFilterValue("sub");

    private Dictionary<string, string> FilterMap => CreateFilterMap();

    #endregion

    #region Helper Methods

    private Dictionary<string, string> CreateFilterMap()
    {
        if (string.IsNullOrEmpty(Filter))
        {
            return new Dictionary<string, string>();
        }

        return Filter.Split('&')
            .Select(part => part.Split('='))
            .Where(parts => parts.Length == 2)
            .ToDictionary(parts => parts[0], parts => parts[1]);
    }

    private string GetFilterValue(string key)
    {
        if (FilterMap.Count == 0)
        {
            return string.Empty;
        }

        return FilterMap.TryGetValue(key, out string value) ? value : string.Empty;
    }

    private int GetLimitFromFilter()
    {
        string limitValue = GetFilterValue("limit");
        return int.TryParse(limitValue, out var value) ? value : DefaultLimit;
    }

    private DateTime ParseDate(string key, DateTime defaultValue)
    {
        string dateValue = GetFilterValue(key);
        return DateTime.TryParse(dateValue, out var value) ? value : defaultValue;
    }

    #endregion
    
}
