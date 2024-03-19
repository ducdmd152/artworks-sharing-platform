using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;

namespace ArtHubRepository.Repository
{
    public class BookmarkRepository : BaseRepository<Bookmark>, IBookmarkRepository
    {
        public BookmarkRepository(IBaseDAO<Bookmark> baseDAO) : base(baseDAO)
        {
        }

        public Bookmark GetByCompositeKey(string email, int postId) => this.DbSet.FirstOrDefault(item => item.AccountEmail == email && item.PostId == postId);
    }
}
