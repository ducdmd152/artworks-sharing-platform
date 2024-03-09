using ArtHubBO.Entities;

namespace ArtHubRepository.Interface
{
    public interface ICategoryRepository : IBaseRepository<Category> 
    {
        IEnumerable<Category> GetCategories();
    }
}
