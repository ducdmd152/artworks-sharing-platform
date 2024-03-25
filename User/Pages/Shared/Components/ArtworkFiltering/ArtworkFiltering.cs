using ArtHubBO.Entities;
using ArtHubService.Interface;
using ArtHubService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace User.Pages.Shared.Components.ArtworkFiltering
{
    public class ArtworkFilteringModel {
        public string SearchValue { get; set; }
        public string OrderByValue { get; set; }
        public IList<CategoryModel> Categories { get; set; }
    }
    [ViewComponent]
    public class ArtworkFiltering : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ArtworkFilteringModel Model { get; set; }
        public ArtworkFiltering(IHttpContextAccessor httpContextAccessor, ICategoryService categoryService)
        {
            _httpContextAccessor = httpContextAccessor;
            Model = new ArtworkFilteringModel();

            Model.SearchValue = _httpContextAccessor.HttpContext.Request.Query["search"];
            Model.OrderByValue = _httpContextAccessor.HttpContext.Request.Query["orderBy"];
            Model.OrderByValue = Model.OrderByValue ?? "1";
            var selectedCategories = _httpContextAccessor.HttpContext.Request.Query["category"].ToList();
            Model.Categories = categoryService.GetCategories()
                                        .Select(item => new CategoryModel
                                        {
                                            Id = item.CategoryId,
                                            Name = item.CategoryName,
                                            IsSelected = selectedCategories.Any(cat => (string)cat == item.CategoryId.ToString())
                                        }).ToList();
        }

        public IViewComponentResult Invoke()
        {
            return View("Default", Model);
        }        
    }
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
