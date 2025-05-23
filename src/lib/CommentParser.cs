using System;
using System.Collections.Generic;

namespace lib;

public abstract class CommentParser
{

    #region Public Methods

    public static CommentBlock[] Parse(string text)
    {
        var result = new List<CommentBlock>();
        var parts = text.Split(["\n\n"], StringSplitOptions.None);

        QuoteBlock lastQuote = null;

        foreach (var originalPart in parts)
        {
            var isQuote = originalPart.StartsWith("&gt;") || originalPart.StartsWith(">");
            var cleanText = originalPart.Replace("&gt;", ">");

            if (isQuote)
            {
                lastQuote = new QuoteBlock { Text = cleanText };
                result.Add(lastQuote);
            }
            else
            {
                var response = new ResponseBlock
                {
                    Text = cleanText,
                    ParentQuoteId = lastQuote?.Id
                };
                result.Add(response);
            }
        }

        return result.ToArray();
    }

    #endregion
    
}