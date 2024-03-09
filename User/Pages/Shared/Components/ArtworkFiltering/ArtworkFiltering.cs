using Microsoft.AspNetCore.Mvc;

namespace User.Pages.Shared.Components.ArtworkFiltering
{
    [ViewComponent]
    public class ArtworkFiltering : ViewComponent
    {
        public List<CategoryModel> Categories { get; set; }

        public ArtworkFiltering()
        {
            Categories = new List<CategoryModel>
            {
                new CategoryModel { Name = "Painting" },
                new CategoryModel { Name = "Sculpture" },
                new CategoryModel { Name = "Photography" },
                // Thêm các danh mục khác nếu cần
            };
        }

        public IViewComponentResult Invoke()
        {
            return View("Default", Categories);
        }
    }
    public class CategoryModel
    {
        public string Name { get; set; }
    }
}
