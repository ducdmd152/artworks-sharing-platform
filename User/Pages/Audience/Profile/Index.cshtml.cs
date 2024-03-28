using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using ArtHubService.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.Audience.Profile
{
    public class IndexModel : PageModel
    {
        private readonly IAudienceService audienceService;

        public IndexModel(IAudienceService audienceService) : base()
        {
            this.audienceService = audienceService;
        }

        public Account Account { get; set; }
        public List<SelectCreatorDTO> Creators { get; set; }
        public PageInfo PageInfo = default!;
        public void OnGet(int pageIndex = 1, int pageSize = 12)
        {
            Account = SessionUtil.GetAuthenticatedAccount(HttpContext);

            var result = audienceService.GetSubcribingCreators(Account.Email ?? string.Empty, pageIndex, pageSize);
            if (result != null)
            {
                Creators = result.PageData;
                PageInfo = result.PageInfo;
            }
        }
    }
}
