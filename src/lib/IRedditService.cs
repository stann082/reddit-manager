using Reddit.Things;

namespace lib;

public interface IRedditService
{
    Task<Task> CacheSavedCommentsAsync();
    Task<Comment[]> GetFilteredCommentsAsync(ICommentOptions options);
}
