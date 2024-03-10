using ArtHubBO.Entities;

namespace ArtHubService.Interface;

public interface ICategoryService
{
    IEnumerable<Category> GetCategories();
}
