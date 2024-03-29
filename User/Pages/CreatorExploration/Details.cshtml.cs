using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using ArtHubService.Utils;
using InventoryManagementGUI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.CreatorExploration
{
    public class DetailsModel : PageModel
    {
        private readonly IArtistService artistService;
        private readonly IPostService postService;
        private readonly IPaypalService paypalService;
        private readonly ISubscribePaidService subscribePaidService;
        private readonly IFeeService feeService;

        public DetailsModel(IArtistService artistService, IPostService postService, IPaypalService paypalService, ISubscribePaidService subscribePaidService, IFeeService feeService) : base()
        {
            this.artistService = artistService;
            this.postService = postService;
            this.paypalService = paypalService;
            this.subscribePaidService = subscribePaidService;
            this.feeService = feeService;
        }

        [BindProperty]
        public string CompleteTransactionCondition { get; set; }
        [BindProperty]
        public string CancelSubscriptionCondition { get; set; }
        [BindProperty]
        public double FeeSubscribe { get; set; }
        public Account Account { get; set; } = default!;
        public SelectCreatorDTO Creator { get; set; } = default!;
        public IList<Post> Posts { get; set; } = default!;
        public SearchPayload<PostAudienceSearchConditionDto> SearchPayload = default!;

        public async Task OnGet(string id, int pageIndex = 1, int pageSize = 2)
        {
            this.CompleteTransactionCondition = "None";
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
            this.FeeSubscribe = this.feeService.GetFeeSubscribe(artistEmail);
        }

        public async Task<IActionResult> OnPostSubscribersAsync(string id)
        {
            Account acc = SessionUtil.GetAuthenticatedAccount(HttpContext);
            if (acc == default)
            {
                return new JsonResult(new
                {
                    Result = "Login",
                    Data = "/Authenticate/Login",
                });
            }

            var artistEmail = Encryption.DecodeKeyToEmail(id);
            HttpContext.Session.SetString("CREATOR_EMAIL", artistEmail);
            var result = await this.paypalService.CreateSubscription(acc.Email, artistEmail).ConfigureAwait(false);
            if (result == string.Empty)
            {
                return new JsonResult(new PostResult
                {
                    Result = Result.Error,
                    Data = "Paypal would fail",
                });
            }

            return new JsonResult(new PostResult
            {
                Result = Result.Ok,
                Data = result,
            });
        }

        public async Task<IActionResult> OnGetSuccess(string subscription_id)
        {
            Account acc = SessionUtil.GetAuthenticatedAccount(HttpContext);
            var creatorEmail = HttpContext.Session.GetString("CREATOR_EMAIL");
            if (creatorEmail == null)
            {
                return RedirectToPage("/CreatorExploration/Index");
            }

            var result = await this.subscribePaidService.SubscribePaidAsync(subscription_id, acc.Email, creatorEmail).ConfigureAwait(false);
            this.CompleteTransactionCondition = result == Result.Error ? "Error" : "Ok";

            await this.LoadDataShowPageAsync(creatorEmail, acc.Email);
            HttpContext.Session.Remove("CREATOR_EMAIL");
            return Page();
        }

        public async Task<IActionResult> OnGetCancelAsync()
        {
            Account acc = SessionUtil.GetAuthenticatedAccount(HttpContext);
            var creatorEmail = HttpContext.Session.GetString("CREATOR_EMAIL");
            if (creatorEmail == null)
            {
                return RedirectToPage("/CreatorExploration/Index");
            }

            this.CompleteTransactionCondition =  "Error";
            await this.LoadDataShowPageAsync(creatorEmail, acc.Email);
            HttpContext.Session.Remove("CREATOR_EMAIL");
            return Page();
        }

        public async Task<IActionResult> OnPostUnSubscriptionAsync(string reason, string creatorEmail)
        {
            Account acc = SessionUtil.GetAuthenticatedAccount(HttpContext);
            if (acc != default)
            {            
                Result result = await this.paypalService.CancelSubscriptionAsync(acc.Email, creatorEmail, reason).ConfigureAwait(false);

                if (result == Result.Error)
                {
                    return new JsonResult(new PostResult
                    {
                        Result = Result.Error,
                        Data = "Paypal would fail",
                    });
                }

                var result2 = await this.subscribePaidService.UnSubAsync(creatorEmail, acc.Email);
                if (result2 == Result.Error)
                {
                    return new JsonResult(new PostResult
                    {
                        Result = Result.Error,
                        Data = "Unsub fail",
                    });
                }

                return new JsonResult(new PostResult
                {
                    Result = Result.Ok,
                    Data = string.Empty,
                });
            }

            return new JsonResult(new PostResult
            {
                Result = Result.Error,
                Data = "Something went wrong",
            });
        }

        private async Task LoadDataShowPageAsync(string creatorEmail, string audienceEmail)
        {
            Creator = artistService.GetCreatorByEmail(creatorEmail, audienceEmail);

            PostAudienceSearchConditionDto condition = new PostAudienceSearchConditionDto()
            {
                ArtistEmail = creatorEmail,
                AudienceEmail = audienceEmail,
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
            this.FeeSubscribe = this.feeService.GetFeeSubscribe(creatorEmail);
        }
    }
}
