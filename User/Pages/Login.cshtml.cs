using ArtHubRepository.Interface;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using User.Pages.Resources;

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
                    ViewData["ErrorMessage"] = MessageResource.AccountBanned;
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
                    case "audience":
                        HttpContext.Session.SetString("CREDENTIAL", accountJson);
                        return RedirectToPage("/Index");
                    case "creator":
                        HttpContext.Session.SetString("CREDENTIAL", accountJson);
                        return RedirectToPage("/CreatorProfile");
                    default:
                        ViewData["ErrorMessage"] = MessageResource.AccountNotHavePermission;
                        return Page();
                }                
            } else
            {
                ViewData["ErrorMessage"] = MessageResource.InvalidUserNamePassword;
                return Page();
            }
        }
    }
}
