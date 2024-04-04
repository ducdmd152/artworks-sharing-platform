using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using ArtHubService.Service;
using ArtHubService.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.CreatorExploration
{
    public class IndexModel : PageModel
    {
        private readonly IArtistService artistService;

        public IndexModel(IArtistService artistService) : base()
        {
            this.artistService = artistService;
        }
        public Account Account { get; set; }
        public List<SelectCreatorDTO> Creators { get; set; }
        public PageInfo PageInfo = default!;
        public void OnGet(int pageIndex = 1, int pageSize = 12)
        {
            Account = SessionUtil.GetAuthenticatedAccount(HttpContext);

            var result = artistService.GetTopCreators(Account?.Email ?? string.Empty, pageIndex, pageSize);
            Creators = result.PageData;
            PageInfo = result.PageInfo;
        }
    }
}
