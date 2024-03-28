using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Principal;

namespace Admin.Pages.Admins
{
    public class EditAccountManagementModel : PageModel
    {

        private readonly IAccountService _accountService;

        public EditAccountManagementModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public Account Account { get; set; }

        [BindProperty]
        public AccountUpdateDto UpdateDto { get; set; }


        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (email == null)
            {
                return NotFound();
            }

            var account =  _accountService.GetAccount(email);

            if (account == null)
            {
                return NotFound();
            }

            Account = account;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string email)
        {


            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }


            bool deleteResult = await _accountService.UpdateAccountStatus(email).ConfigureAwait(false);

            if (deleteResult)
            {

                return RedirectToPage();
            }
            else
            {

                return Page();
            }
        }
    }
    
}
