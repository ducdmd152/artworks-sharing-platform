using ArtHubBO.DTO;
using ArtHubService.Interface;
using ArtHubService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.Authenticate
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IEmailService emailService;

        public ForgotPasswordModel(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public void OnPost()
        {
            emailService.SendEmail(new SendEmailDto
            {
                Subject = "Huhu",
                ToEmail = "hoangtienbmt2911@gmail.com",
                Body = "<html><body> hehe </body></html>"
            });
        }
    }
}
