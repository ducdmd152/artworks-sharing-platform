using ArtHubBO.DTO;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using ArtHubService.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.Audience.Profile
{
    public class SavedModel : PageModel
    {
        private readonly IPostService postService;

        public SavedModel(IPostService postService) : base()
        {
            this.postService = postService;
        }

        public IList<SelectPostDTO> Posts { get; set; } = default!;
        public PageInfo PageInfo = default!;


        public async Task OnGetAsync(int pageIndex = 1, int pageSize = 12)
        {
            var result = await postService.GetBookmarkedPostList(SessionUtil.GetAuthenticatedAccount(this.HttpContext)?.Email, pageIndex, pageSize);
            Posts = result.PageData;
            PageInfo = result.PageInfo;
        }
    }
}
