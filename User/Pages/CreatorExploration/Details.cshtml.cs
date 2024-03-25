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
        private readonly IPaypalService paypalService;
        private readonly ISubscribePaidService subscribePaidService;

        public DetailsModel(IArtistService artistService, IPostService postService, IPaypalService paypalService, ISubscribePaidService subscribePaidService) : base()
        {
            this.artistService = artistService;
            this.postService = postService;
            this.paypalService = paypalService;
            this.subscribePaidService = subscribePaidService;
        }

        [BindProperty]
        public string CompleteTransaction { get; set; }
        public Account Account { get; set; } = default!;
        public SelectCreatorDTO Creator { get; set; } = default!;
        public IList<Post> Posts { get; set; } = default!;
        public SearchPayload<PostAudienceSearchConditionDto> SearchPayload = default!;

        public async Task OnGet(string id, int pageIndex = 1, int pageSize = 12)
        {
            this.CompleteTransaction = "None";
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

        public async Task<IActionResult> OnPostSubscribersAsync(string id)
        {
            Account acc = SessionUtil.GetAuthenticatedAccount(HttpContext);
            if (acc == default)
            {
                return Redirect("/Authenticate/Login");
            }

            var artistEmail = Encryption.DecodeKeyToEmail(id);
            HttpContext.Session.SetString("CREATOR_EMAIL", artistEmail);
            var result = await this.paypalService.CreateSubscription(acc.Email, artistEmail).ConfigureAwait(false);
            
            return new RedirectResult(result);
        }

        public async Task<IActionResult> OnGetSuccess()
        {
            Account acc = SessionUtil.GetAuthenticatedAccount(HttpContext);
            var creatorEmail = HttpContext.Session.GetString("CREATOR_EMAIL");
            var result = await this.subscribePaidService.SubscribePaidAsync(acc.Email, creatorEmail).ConfigureAwait(false);
            this.CompleteTransaction = result == Result.Ok ? "Ok" : "Error";
            
            Creator = artistService.GetCreatorByEmail(creatorEmail, acc.Email);

            PostAudienceSearchConditionDto condition = new PostAudienceSearchConditionDto()
            {
                ArtistEmail = creatorEmail,
                AudienceEmail = acc.Email,
                PostStatus = PostStatus.Approval,
                SortType = SortType.RECENT,
                SortDirection = SortDirection.DESC,
            };

            SearchPayload = new SearchPayload<PostAudienceSearchConditionDto>()
            {
                PageInfo = new PageInfo()
                {
                    PageNum = 1,
                    PageSize = 12,
                },
                SearchCondition = condition
            };

            Posts = await postService.GetAllPostBySearchConditionForAudienceAsync(SearchPayload);

            return Page();
        }

        public async Task<IActionResult> OnGetCancelAsync()
        {
            Account acc = SessionUtil.GetAuthenticatedAccount(HttpContext);
            var creatorEmail = HttpContext.Session.GetString("CREATOR_EMAIL");
            this.CompleteTransaction =  "Error";
            
            Creator = artistService.GetCreatorByEmail(creatorEmail, acc.Email);

            PostAudienceSearchConditionDto condition = new PostAudienceSearchConditionDto()
            {
                ArtistEmail = creatorEmail,
                AudienceEmail = acc.Email,
                PostStatus = PostStatus.Approval,
                SortType = SortType.RECENT,
                SortDirection = SortDirection.DESC,
            };

            SearchPayload = new SearchPayload<PostAudienceSearchConditionDto>()
            {
                PageInfo = new PageInfo()
                {
                    PageNum = 1,
                    PageSize = 12,
                },
                SearchCondition = condition
            };

            Posts = await postService.GetAllPostBySearchConditionForAudienceAsync(SearchPayload);

            return Page();
        }
    }
}
