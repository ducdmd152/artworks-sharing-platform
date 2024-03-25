using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using ArtHubService.Service;
using ArtHubService.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
namespace User.Pages.CreatorExploration
{
    public class DetailsModel : PageModel
    {
        private readonly IArtistService artistService;
        private readonly IPostService postService;

        public DetailsModel(IArtistService artistService, IPostService postService) : base()
        {
            this.artistService = artistService;
            this.postService = postService;
        }

        public Account Account { get; set; } = default!;
        public SelectCreatorDTO Creator { get; set; } = default!;
        public IList<Post> Posts { get; set; } = default!;
        public SearchPayload<PostAudienceSearchConditionDto> SearchPayload = default!;

        public async Task OnGet(string id, int pageIndex = 1, int pageSize = 12)
        {
            Account = SessionUtil.GetAuthenticatedAccount(HttpContext);
            var artistEmail = Encryption.DecodeKeyToEmail(id);
            var audienceEmail = Account?.Email ?? string.Empty;

            Creator = artistService.GetCreatorByEmail(artistEmail, audienceEmail);

            PostAudienceSearchConditionDto condition = new PostAudienceSearchConditionDto()
            {
                ArtistEmail = artistEmail,
                AudienceEmail = audienceEmail,
                PostStatus = PostStatus.Approval,
                SortType = SortType.RECENT,
                SortDirection = SortDirection.DESC,
            };

            SearchPayload = new SearchPayload<PostAudienceSearchConditionDto>()
            {
                PageInfo = new PageInfo()
                {
                    PageNum = pageIndex,
                    PageSize = pageSize,
                },
                SearchCondition = condition
            };

            Posts = await postService.GetAllPostBySearchConditionForAudienceAsync(SearchPayload);
        }
    }
}
