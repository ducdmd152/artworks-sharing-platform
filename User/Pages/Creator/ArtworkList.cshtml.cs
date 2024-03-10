using ArtHubBO.DTO;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.Creator
{
    public class ArtworkListModel : PageModel
    {
        private readonly IPostService _postService;

        [BindProperty]
        public SearchPayload<PostSearchConditionDto> SearchPayload { get; set; } = new SearchPayload<PostSearchConditionDto>(new PageInfo(1, 8), new PostSearchConditionDto());

        public ArtworkListModel(IPostService postService)
        {
            _postService = postService;
        }

        public async Task OnGetAsync()
        {
            var output = await _postService.GetAllPostBySearchConditionAsync(SearchPayload);
            return;
        }
    }
}
