using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;

namespace ArtHubRepository.Repository;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(IBaseDAO<Category> baseDAO) : base(baseDAO)
    {
    }

    public IEnumerable<Category> GetCategories()
    {
        return this.DbSet.ToList();
    }
}
