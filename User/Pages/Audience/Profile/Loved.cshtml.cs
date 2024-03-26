using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using ArtHubService.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.Audience.Profile
{
    public class LovedModel : PageModel
    {
        private readonly IPostService postService;

        public LovedModel(IPostService postService) : base()
        {
            this.postService = postService;
        }

        public Account Account { get; set; }
        public IList<SelectPostDTO> Posts { get; set; } = default!;
        public PageInfo PageInfo = default!;


        public async Task OnGetAsync(int pageIndex = 1, int pageSize = 12)
        {
            Account = SessionUtil.GetAuthenticatedAccount(HttpContext);

            var result = await postService.GetReactedPostList(SessionUtil.GetAuthenticatedAccount(this.HttpContext)?.Email, pageIndex, pageSize);
            Posts = result.PageData;
            PageInfo = result.PageInfo;
        }
    }
}
