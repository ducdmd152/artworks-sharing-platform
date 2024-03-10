using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace User.Pages.Shared.Components.ArtworkFiltering
{
    public class ArtworkFilteringModel {
        public string SearchValue { get; set; }
        public string OrderByValue { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
    [ViewComponent]
    public class ArtworkFiltering : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ArtworkFilteringModel Model { get; set; }
        public ArtworkFiltering(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            Model = new ArtworkFilteringModel();

            Model.SearchValue = _httpContextAccessor.HttpContext.Request.Query["search"];
            Model.OrderByValue = _httpContextAccessor.HttpContext.Request.Query["orderBy"];
            Model.OrderByValue = Model.OrderByValue ?? "1";
            Model.Categories = new List<CategoryModel>
            {
                new CategoryModel { Name = "Painting" },
                new CategoryModel { Name = "Sculpture" },
                new CategoryModel { Name = "Photography" },
                // Thêm các danh mục khác nếu cần
            };
        }

        public IViewComponentResult Invoke()
        {
            return View("Default", Model);
        }
    }
    public class CategoryModel
    {
        public string Name { get; set; }
    }
}
