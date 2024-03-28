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

        public async Task<IActionResult> OnPostAsync()
        {
           

            try
            {
                var result = await _accountService.UpdateAccountFields(Account.Email, UpdateDto); // Sử dụng Account.Email thay vì tham số email

                if (!result)
                {
                    // Xử lý nếu cập nhật không thành công
                    // Ví dụ: Hiển thị thông báo lỗi
                    ModelState.AddModelError("", "Cập nhật tài khoản không thành công.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                // Ví dụ: Hiển thị thông báo lỗi
                ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật tài khoản.");
                return Page();
            }

            // Chuyển hướng sau khi cập nhật thành công
            return RedirectToPage("./AccountManagement");
        }
    }
}
