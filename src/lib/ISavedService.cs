using Reddit.Things;

namespace lib;

public interface ISavedService
{
    Task<Comment[]> GetFilteredCommentsAsync(IOptions options);
}
