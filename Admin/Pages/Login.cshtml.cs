using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Admin.Pages
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
            if (role != null && role.Equals("admin"))
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
                // Configure JsonSerializerSettings to handle reference loops
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var accountJson = JsonConvert.SerializeObject(account, settings);
                switch (role)
                {                    
                    case "moderator":
                        HttpContext.Session.SetString("CREDENTIAL", accountJson);                        
                        HttpContext.Session.SetString("email", account.Email);
                        HttpContext.Session.SetString("firstname", account.FirstName);
                        return RedirectToPage("/ModHomePage");
                    case "admin":
                        HttpContext.Session.SetString("CREDENTIAL", accountJson);
                        HttpContext.Session.SetString("email", account.Email);
                        HttpContext.Session.SetString("firstname", account.FirstName);
                        return RedirectToPage("/AdminHomePage");
                    default:
                        ViewData["ErrorMessage"] = "Invalid username or password.";
                        return Page();
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Invalid username or password.";
                return Page();
            }
        }
    }
}
