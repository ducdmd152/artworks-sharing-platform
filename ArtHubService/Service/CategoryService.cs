using ArtHubBO.Entities;
using ArtHubRepository.Interface;
using ArtHubService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubService.Service;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }

    public IEnumerable<Category> GetCategories()
    {
        return categoryRepository.GetCategories();
    }
}
