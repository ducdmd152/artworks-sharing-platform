using ArtHubBO.Entities;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Admins
{
    public class AccountDetailManagementModel : PageModel
    {
        private readonly IAccountService _accountService;

        public AccountDetailManagementModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public Account Account { get; set; }

        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return NotFound();
            }

            Account = _accountService.GetAccount(email);

            if (Account == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
