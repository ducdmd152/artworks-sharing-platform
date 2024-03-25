using System.Diagnostics;
using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using ArtHubService.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPostService postService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IPostService postService, ILogger<IndexModel> logger) : base()
        {
            this.postService = postService;
            _logger = logger;
        }

        public Account Account { get; set; } = default!;
        public IList<Post> Posts { get; set; } = default!;
        public SearchPayload<PostAudienceSearchConditionDto> SearchPayload = default!;


        public async Task OnGetAsync(int pageIndex = 1, int pageSize = 12, string? search = "", int orderBy = 1, int[]? category = null)
        {
            Account = SessionUtil.GetAuthenticatedAccount(HttpContext);
            PostAudienceSearchConditionDto condition = new PostAudienceSearchConditionDto()
            {
                AudienceEmail = Account?.Email ?? string.Empty,
                PostStatus = PostStatus.Approval,
                SortType = orderBy == (int)SortType.FAVOURITE ? SortType.FAVOURITE : SortType.RECENT,
                SortDirection = SortDirection.DESC,
                Title = search,
                CategoryId = category
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
