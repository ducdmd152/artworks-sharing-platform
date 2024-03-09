using ArtHubBO.DTO;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages
{
    public class CreatorArtworkListModel : PageModel
    {
        private readonly IPostService _postService;

        [BindProperty]
        public SearchPayload<PostSearchConditionDto> SearchPayload { get; set; } = new SearchPayload<PostSearchConditionDto>(new PageInfo(1, 8), new PostSearchConditionDto());

        public CreatorArtworkListModel(IPostService postService)
        {
            _postService = postService;
        }

        public void OnGet()        
        {
            var output = _postService.GetAllPostBySearchCondition(SearchPayload);
            string a = "a";
            return;
        }
    }
}
