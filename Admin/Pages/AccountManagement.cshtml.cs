using ArtHubBO.Entities;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages
{
    public class AccountManagementModel : PageModel
    {
        private readonly IAccountService _accountService;

       

       public  AccountManagementModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IEnumerable<Account> account { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_accountService.GetAccounts != null)
            {
                account = _accountService.GetAccounts();
            }
        }
    }
}
