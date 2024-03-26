using ArtHubBO.Entities;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public async Task<IActionResult> OnPostAsync(string email)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //var result = await _accountService.UpdateAccount(email);

            //if (!result)
            //{
            //    // Handle update failure
            //    ModelState.AddModelError("", "Failed to update account. Please try again.");
            //    return Page();
            //}

            return RedirectToPage("./Index"); // Redirect to account list page after successful update
        }
    }
}
