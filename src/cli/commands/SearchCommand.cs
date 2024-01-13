using lib;

namespace cli.commands;

public class SearchCommand : AbstractCommand
{

    #region Constructors

    public SearchCommand(ISearchOptions options, ISearchService service)
        : base(string.Empty)
    {
        _options = options;
        _service = service;
    }

    #endregion

    #region Variables

    private readonly ISearchOptions _options;
    private readonly ISearchService _service;

    #endregion

    #region Public Methods

    public async Task<int> Execute()
    {
        Console.Write("Fetching records, please wait...");
        var comments = await _service.Search(_options);
        return await Task.FromResult(0);
    }

    #endregion

    #region Helper Methods

    private static void HighlightMatches(string text, string query)
    {
        var queryLength = query.Length;
        int startIndex = 0;
        int index = text.IndexOf(query, StringComparison.OrdinalIgnoreCase);

        while (index != -1)
        {
            Console.Write(text.Substring(startIndex, index - startIndex));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text.Substring(index, queryLength));
            Console.ResetColor();

            startIndex = index + queryLength;
            index = text.IndexOf(query, startIndex, StringComparison.OrdinalIgnoreCase);
        }

        Console.Write(text[startIndex..]);
        Console.WriteLine();
    }

    #endregion

}
