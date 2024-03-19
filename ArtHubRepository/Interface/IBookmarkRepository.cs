using ArtHubBO.Entities;

namespace ArtHubRepository.Interface
{
    public interface IBookmarkRepository : IBaseRepository<Bookmark>
    {
        Bookmark GetByCompositeKey(string email, int postId);
    }
}
