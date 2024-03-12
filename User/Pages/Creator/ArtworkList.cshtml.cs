using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace User.Pages.Creator
{
    public class ArtworkListModel : PageModel
    {
        private readonly IPostService postService;

        private readonly ICategoryService categoryService;

        [BindProperty]
        public SearchPayload<PostSearchConditionDto> SearchPayload { get; set; } = new SearchPayload<PostSearchConditionDto>(new PageInfo(1, 8), new PostSearchConditionDto());

        [BindProperty]
        public List<Category> Categories { get; set; }

        public ArtworkListModel(IPostService postService, ICategoryService categoryService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
        }

        public async Task OnGetAsync()
        {
            var accountEmail = HttpContext.Session.GetString("ACCOUNT_EMAIL");     
            if (accountEmail != null)
            {
                SearchPayload.SearchCondition.ArtistEmail = accountEmail;
            }
            Categories = categoryService.GetCategories().ToList();                        
            var output = await postService.GetAllPostBySearchConditionAsync(SearchPayload);
            return;
        }
    }
}
