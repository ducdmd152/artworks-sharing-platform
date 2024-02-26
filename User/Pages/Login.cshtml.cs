using ArtHubRepository.Interface;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService accountService;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public LoginModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult OnGet()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != null && (role.Equals("audience") || role.Equals("creator")))
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }


        public IActionResult OnPost()
        {
            var account = accountService.GetAccountByUsernameAndPassword(Email, Password);
            if (account != null)
            {
                if (!account.Enabled)
                {
                    ViewData["ErrorMessage"] = "Your account has been banned.";
                    return Page();
                }
                string role = account.Role.RoleName;
                switch(role)
                {
                    case "audience":
                        HttpContext.Session.SetString("role", "audience");
                        HttpContext.Session.SetString("email", account.Email);
                        HttpContext.Session.SetString("firstname", account.FirstName);
                        return RedirectToPage("/Index");
                    case "creator":
                        HttpContext.Session.SetString("role", "creator");
                        HttpContext.Session.SetString("email", account.Email);
                        HttpContext.Session.SetString("firstname", account.FirstName);
                        return RedirectToPage("/CreatorHomePage");
                    default:
                        ViewData["ErrorMessage"] = "Invalid username or password.";
                        return Page();
                }                
            } else
            {
                ViewData["ErrorMessage"] = "Invalid username or password.";
                return Page();
            }
        }
    }
}
