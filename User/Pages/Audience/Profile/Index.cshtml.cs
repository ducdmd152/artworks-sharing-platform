using ArtHubBO.Entities;
using ArtHubService.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.Audience.Profile
{
    public class IndexModel : PageModel
    {
        public Account Account { get; set; }
        public void OnGet()
        {
            Account = SessionUtil.GetAuthenticatedAccount(HttpContext);
        }
    }
}
