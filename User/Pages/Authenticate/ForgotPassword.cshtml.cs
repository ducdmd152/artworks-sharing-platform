using ArtHubBO.DTO;
using ArtHubService.Interface;
using ArtHubService.Service;
using ArtHubService.Utils;
using InventoryManagementGUI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using User.Pages.Filter;

namespace User.Pages.Authenticate
{
    public class ForgotPasswordModel : PageModel
    {
        [BindProperty]
        public PasswordConfirmDto PasswordConfirm { get; set; } = null!;
        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [BindProperty]
        public string Code { get; set; } = "";
        [BindProperty]
        [Required(ErrorMessage = "Status is required.")]
        public int Status { get; set; } = 1;
        private readonly IEmailService emailService;
        private readonly IAccountService accountService;

        public ForgotPasswordModel(IEmailService emailService, IAccountService accountService)
        {
            this.emailService = emailService;
            this.accountService = accountService;
        }

        public async Task<IActionResult> OnPost()
        {
            if (Status == 1)
            {
                var account = accountService.GetAccountByEmail(Email);
                if (account == null)
                {
                    TempData["NotExistEmailMessage"] = "Not exist this email in system!";
                    return Page();
                } else
                {
                    var isSendMail = emailService.SendEmail(new SendEmailDto
                    {
                        Subject = "Your ArtHub account recovery code",
                        ToEmail = account.Email,
                        Body = @"
                                <html>
                                    <body style='font-family: Arial, sans-serif; color: #333;'>
                                        <div style='margin-bottom: 20px;'>
                                            <img src='https://d28yx6l5j59h9f.cloudfront.net/Artwork/01732ec7-756b-4621-94c1-2da9a8647be0.png' alt='ArtHub Logo' style='display: block; margin: 0 auto;' />
                                        </div>
                                        <p>Hello,</p>
                                        <p>We have received a request to reset your password for the ArtHub app. Please use the following code to reset your password:</p>
                                        <div style='text-align: center; margin: 20px;'>
                                            <span style='font-size: 24px; padding: 10px; border: 1px solid #ccc;'>" + account.Password + @"</span>
                                        </div>
                                        <p>If you did not request a password reset, please ignore this email or contact support.</p>
                                    </body>
                                </html>"
                    });
                    if (!isSendMail)
                    {
                        TempData["FailSendMail"] = "Send mail fail!";
                        return Page();
                    }
                    TempData["SuccessSendMail"] = "Send mail success!";
                    Status = 2;
                    return Page();
                }
            } else if (Status == 2)
            {
                var account = accountService.GetAccountByEmail(Email);
                if (account == null)
                {
                    TempData["NotExistEmailMessage"] = "Not exist this email in system!";
                    return Page();
                }
                else
                {
                    if (account.Password == Code)
                    {
                        TempData["CorrectCode"] = "Input correct code!";
                        Status = 3;
                        return Page();
                    } else
                    {
                        TempData["NotCorrectCode"] = "Input incorrect code!";
                        return Page();
                    }
                }
            } else
            {
                var account = accountService.GetAccountByEmail(Email);
                if (account == null)
                {
                    TempData["NotExistEmailMessage"] = "Not exist this email in system!";
                    return Page();
                }
                else
                {
                    if (account.Password == Code && account.Email == Email)
                    {
                        PasswordConfirm.NewPassword = Encryption.Encrypt(PasswordConfirm.NewPassword);
                        PasswordConfirm.ConfirmPassword = Encryption.Encrypt(PasswordConfirm.ConfirmPassword);
                        bool isUpdate = await accountService.ChangePassword(PasswordConfirm, account.Email);
                        if (isUpdate)
                        {
                            TempData["ChangePasswordSuccess"] = "Your change password was successful! Please proceed to login.";
                            return RedirectToPage(URIConstant.Login);
                        } else
                        {
                            TempData["ChangePasswordFail"] = "Change password fail!";
                            return Page();
                        }
                    } else
                    {
                        TempData["ChangePasswordFail"] = "Change password fail!";
                        return Page();
                    }                   
                }
            }
        }
    }
}
