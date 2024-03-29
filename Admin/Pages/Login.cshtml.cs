using Admin.Pages.Resources;
using ArtHubBO.Entities;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Security.Principal;

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
            if (HttpContext?.Session == null)
            {
                Console.WriteLine("No session");
            }            
        }

        public void OnGet()
        {
            if (HttpContext != null && HttpContext.Session != null)
            {
                var userString = HttpContext.Session.GetString("CREDENTIAL");
                Console.WriteLine(userString);
                var userConvert = userString != null ? JsonConvert.DeserializeObject<Account>(userString) : null;
                if (userConvert != null)
                {
                    Console.WriteLine(userConvert.Role.RoleId + " " + userConvert.Role.RoleName);
                    switch (userConvert.Role.RoleName)
                    {
                        case "Moderator":
                            HttpContext.Response.Redirect("/Moderator/ArtWorksManagement");
                            break;
                        case "Admin":
                            HttpContext.Response.Redirect("/Admins/Dashboard");
                            break;
                    }
                }
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
                    case "Moderator":
                        HttpContext.Session.SetString("CREDENTIAL", accountJson);
                        HttpContext.Session.SetString("ACCOUNT_EMAIL", account.Email);
                        return RedirectToPage("/Moderator/ArtWorksManagement");
                    case "Admin":
                        HttpContext.Session.SetString("CREDENTIAL", accountJson);
                        HttpContext.Session.SetString("ACCOUNT_EMAIL", account.Email);
                        return RedirectToPage("/Admins/Dashboard");
                    default:
                        ViewData["ErrorMessage"] = MessageResource.AccountNotHavePermission;
                        return Page();
                }
            }
            else
            {
                ViewData["ErrorMessage"] = MessageResource.InvalidUserNamePassword;
                return Page();
            }
        }
    }
}
