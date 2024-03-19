using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using InventoryManagementGUI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace User.Pages.Artworks
{
    public class DetailsModel : PageModel
    {
        private readonly IPostService postService;
        private readonly IInteractionService interactionService;
        private readonly IReportService reportService;
        
        public DetailsModel(IPostService postService, IInteractionService interactionService, IReportService reportService) : base()
        {
            this.postService = postService;
            this.interactionService = interactionService;
            this.reportService = reportService;
        }
        public Post Post { get; set; } = default!;
        public List<Post> ArtistSuggestion = default!;
        public List<Post> OtherSuggestion = default!;
        public bool IsReacted { get; set; }
        public bool IsBookmarked { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            } 

            var item = postService.Get(id);
            if (item == null || (item.Status != (int)PostStatus.Approval))
            {
                return NotFound();
            }
            else
            {
                Post = item;
                var email = this.GetEmailAccountLogin();
                IsReacted = interactionService.CheckIsReactedForPost(email, id);
                IsBookmarked = interactionService.CheckIsBookmarkedForPost(email, id);
                await interactionService.OneMoreView(id).ConfigureAwait(false);

                ArtistSuggestion = await postService.GetAllPostBySearchConditionAsync(new SearchPayload<PostSearchConditionDto>()
                {
                    PageInfo = new PageInfo()
                    {
                        PageNum = 1,
                        PageSize = 3,
                    },
                    SearchCondition = new PostSearchConditionDto()
                    {
                        ArtistEmail = item.ArtistEmail,
                    }
                });

                OtherSuggestion = await postService.GetAllPostBySearchConditionAsync(new SearchPayload<PostSearchConditionDto>()
                {
                    PageInfo = new PageInfo()
                    {
                        PageNum = 1,
                        PageSize = 4,
                    },
                    SearchCondition = new PostSearchConditionDto()
                    {
                        PostScope = PostScope.Public,
                        PostStatus = PostStatus.Approval,
                        SortType = SortType.FAVOURITE,
                        SortDirection = SortDirection.DESC,
                        //CategoryId = Post.PostCategories.Select(item => item.CategoryId).ToArray(),
                    }
                });
            }
            return Page();
        }

        public async Task<IActionResult> OnPostReactAsync(int postId)
        {
            var email = this.GetEmailAccountLogin();
            if (email == null)
            {
                return new UnauthorizedResult();
            }

            await interactionService.ReactForPost(email, postId);
            return new OkResult();
        }

        public async Task<IActionResult> OnPostUnReactAsync(int postId)
        {
            var email = this.GetEmailAccountLogin();
            if (email == null)
            {
                return new UnauthorizedResult();
            }

            await interactionService.UnReactForPost(email, postId);
            return new OkResult();
        }

        public async Task<IActionResult> OnPostBookmarkAsync(int postId)
        {
            var email = this.GetEmailAccountLogin();
            if (email == null)
            {
                return new UnauthorizedResult();
            }

            await interactionService.BookmarkForPost(email, postId);
            return new OkResult();
        }

        public async Task<IActionResult> OnPostUnBookmarkAsync(int postId)
        {
            var email = this.GetEmailAccountLogin();
            if (email == null)
            {
                return new UnauthorizedResult();
            }

            await interactionService.UnBookmarkForPost(email, postId);
            return new OkResult();
        }

        public async Task<IActionResult> OnPostReportAsync([FromBody] ReportDetail reportDetail)
        {
            if (this.GetEmailAccountLogin() == null)
            {
                return new JsonResult(new PostResult
                {
                    Result = Result.Error,
                    Data = string.Empty,
                });
            }
            Result result = await this.reportService.Register(reportDetail.PostId, reportDetail.Reason, this.GetEmailAccountLogin());
            return new JsonResult(new PostResult
            {
                Result = result,
                Data = string.Empty,
            });
        }

        private string GetEmailAccountLogin()
        {
            var userString = HttpContext.Session.GetString("CREDENTIAL");
            var user = userString != null ? JsonConvert.DeserializeObject<Account>(userString) : null;
            if (user == null)
            {
                return null;
            }

            return user.Email;
        }
    }
}

public class ReportDetail
{
    public int PostId { get; set; }
    public string Reason { get; set; }
}
