using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.Audience
{
    public class IndexModel : PageModel
    {
        private readonly IPostService postService;

        public IndexModel(IPostService postService) : base()
        {
            this.postService = postService;
        }

        public IList<Post> Posts { get; set; } = default!;
        public SearchPayload<PostSearchConditionDto> SearchPayload = default!;


        public void OnGet(int pageIndex = 1, int pageSize = 12, string? search = "", int? orderBy = null, int[]? category = null)
        {
            PostSearchConditionDto condition = new PostSearchConditionDto()
            {
                PostScope = PostScope.Public,
                PostStatus = PostStatus.Approval,
                SortType = orderBy == (int)SortType.FAVOURITE ? SortType.FAVOURITE : SortType.RECENT,
                SortDirection = SortDirection.DESC,
                CategoryId = category
            };

            SearchPayload = new SearchPayload<PostSearchConditionDto>()
            {
                PageInfo = new PageInfo()
                {
                    PageNum = pageIndex,
                    PageSize = pageSize,
                },
                SearchCondition = condition
            };

            Posts = postService.GetAllPostBySearchCondition(SearchPayload);
        }
    }
}
