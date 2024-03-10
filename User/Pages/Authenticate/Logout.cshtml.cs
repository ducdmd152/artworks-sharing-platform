using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using User.Pages.Filter;

namespace User.Pages.Authenticate
{
    public class LogoutModel : PageModel
    {
        private readonly IDistributedCache _cache;

        public LogoutModel(IDistributedCache cache)
        {
            _cache = cache;
        }

        public IActionResult OnGet()
        {            
            // Clear session
            HttpContext.Session.Clear();
            return RedirectToPage(URIConstant.HomePage);
        }
    }
}
