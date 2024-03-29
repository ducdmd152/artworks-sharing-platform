using ArtHubBO.Entities;
using ArtHubRepository.Interface;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using User.Pages.Filter;
using User.Pages.Resources;

namespace User.Pages.Authenticate
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

        public void OnGet()
        {
            if (HttpContext != null && HttpContext.Session != null)
            {
                var userString = HttpContext.Session.GetString("CREDENTIAL");
                var userConvert = userString != null ? JsonConvert.DeserializeObject<Account>(userString) : null;
                if (userConvert != null)
                    HttpContext.Response.Redirect(URIConstant.HomePage);
            }
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
                    case "Audience":
                        HttpContext.Session.SetString("CREDENTIAL", accountJson);
                        HttpContext.Session.SetString("ACCOUNT_EMAIL", account.Email);
                        return RedirectToPage("/Index");
                    case "Creator":
                        HttpContext.Session.SetString("CREDENTIAL", accountJson);
                        HttpContext.Session.SetString("ACCOUNT_EMAIL", account.Email);
                        return RedirectToPage("/Creator/Profile");
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
