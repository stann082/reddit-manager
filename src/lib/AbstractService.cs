using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace lib;

public abstract class AbstractService
{

    #region Enums

    private enum SortOption
    {
        Date,
        Score
    }

    #endregion
    
    #region Shared Methods

    protected static IEnumerable<CommentPreview> FilterComments(IEnumerable<CommentPreview> comments, IOptions options)
    {
        string id = options.Id;
        if (!string.IsNullOrEmpty(id))
        {
            return comments.Where(c => c.Id == id).ToArray();
        }

        IEnumerable<CommentPreview> filteredComments = comments;

        if (options.ShouldExcludeArchived)
        {
            filteredComments = filteredComments.Where(c => !c.IsArchive);
        }
        
        if (options.StartDate > DateTime.MinValue)
        {
            filteredComments = filteredComments.Where(c => c.Date >= options.StartDate);
        }
        
        if (options.StopDate < DateTime.MaxValue)
        {
            filteredComments = filteredComments.Where(c => c.Date <= options.StopDate);
        }
        
        if (!string.IsNullOrEmpty(options.Query))
        {
            if (options.IsExactWord)
            {
                string pattern = @"\b" + Regex.Escape(options.Query) + @"\b";
                filteredComments = filteredComments.Where(c => Regex.IsMatch(c.Body, pattern, RegexOptions.IgnoreCase));
            }
            else
            {
                filteredComments = filteredComments.Where(c => c.Body.Contains(options.Query, StringComparison.OrdinalIgnoreCase));
            }
        }

        if (!options.IsFilterEnabled)
        {
            return filteredComments;
        }

        string author = options.Author;
        if (!string.IsNullOrEmpty(author))
        {
            filteredComments = filteredComments.Where(c => c.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
        }

        string sub = options.Subreddit;
        if (!string.IsNullOrEmpty(sub))
        {
            filteredComments = filteredComments.Where(c => c.Subreddit.Equals(sub, StringComparison.OrdinalIgnoreCase));
        }

        if (options.SortByDate)
        {
            filteredComments = ApplySorting(filteredComments, SortOption.Date, options.IsDescending);
        }
        else if (options.SortByScore)
        {
            filteredComments = ApplySorting(filteredComments, SortOption.Score, options.IsDescending);
        }
        
        return filteredComments;
    }

    private static IEnumerable<CommentPreview> ApplySorting(IEnumerable<CommentPreview> comments, SortOption sortOption, bool isDescending)
    {
        return sortOption switch
        {
            SortOption.Date => isDescending ? comments.OrderByDescending(c => c.Date) : comments.OrderBy(c => c.Date),
            SortOption.Score => isDescending ? comments.OrderByDescending(c => c.Score) : comments.OrderBy(c => c.Score),
            _ => comments
        };
    }

    #endregion
    
}