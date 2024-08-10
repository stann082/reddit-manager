using CommandLine;

namespace cli.options;

public abstract class AbstractOptions
{

    #region Constants

    private const int DefaultLimit = 25;

    #endregion
    
    #region Properties

    [Option('c', "comment", Default = true, HelpText = "Specify if you're searching for comment.")]
    public bool Comment { get; set; }
    
    [Option('e', "exact", HelpText = "Specify if you're searching for exact word in a query.")]
    public bool IsExactWord { get; set; }
    
    [Option('p', "post", HelpText = "Specify if you're searching for post.")]
    public bool Post { get; set; }
    
    [Option('f', "filter", HelpText = "Filters by sub, author, date (e.g., -f author=foomanchu&sub=news.")]
    public string Filter { get; set; }
    
    [Option('q', "query", HelpText = "Search for a specific word.")]
    public string Query { get; set; }
    
    [Option("show-id", HelpText = "Display comment id.")]
    public bool ShowId { get; set; }
    
    [Option('u', "user", HelpText = "Specify a user (if blank your personal account will be used).")]
    public string User { get; set; }

    #endregion

    #region Properties

    private Dictionary<string, string> FilterMap => CreateFilterMap();
    public int Limit => GetLimitFromFilter();

    #endregion
    
    #region Public Methods

    public string GetFilterValue(string key)
    {
        if (!FilterMap.Any())
        {
            return string.Empty;
        }
        
        return FilterMap.TryGetValue(key, out string value) ? value : string.Empty;
    }

    #endregion

    #region Helper Methods

    private Dictionary<string,string> CreateFilterMap()
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

    private int GetLimitFromFilter()
    {
        string limitValue = GetFilterValue("limit");
        return int.TryParse(limitValue, out var value) ? value : DefaultLimit;
    }

    #endregion

}
