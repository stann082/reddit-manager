using System.Text.RegularExpressions;

namespace lib;

public abstract class AbstractService
{
    
    #region Shared Methods

    protected static IEnumerable<CommentPreview> FilterComments(IEnumerable<CommentPreview> comments, IOptions options)
    {
        string id = options.Id;
        if (!string.IsNullOrEmpty(id))
        {
            return comments.Where(c => c.Id == id).ToArray();
        }

        IEnumerable<CommentPreview> filteredComments = comments;

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

        if (options.IsDescending)
        {
            filteredComments = filteredComments.OrderByDescending(c => c.Date);
        }

        if (options.ScoreGreaterThan != int.MinValue)
        {
            filteredComments = filteredComments.Where(c => c.Score >= options.ScoreGreaterThan);
        }
        else if (options.ScoreLessThan != int.MaxValue)
        {
            filteredComments = filteredComments.Where(c => c.Score <= options.ScoreLessThan);
        }
        
        return filteredComments;
    }

    #endregion
    
}