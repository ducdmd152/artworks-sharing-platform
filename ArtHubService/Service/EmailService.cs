using ArtHubBO.DTO;
using ArtHubService.Interface;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using ArtHubBO.Constants;

namespace ArtHubService.Service;

public class EmailService : IEmailService
{
    private readonly IConfiguration configuration;

    public EmailService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public bool SendEmail(SendEmailDto sendEmailDto)
    {
        try
        {
            string fromMail = configuration[EmailConstants.Email] ?? "";
            string fromPassword = configuration[EmailConstants.Password] ?? "";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = sendEmailDto.Subject;
            message.To.Add(new MailAddress(sendEmailDto.ToEmail));
            message.Body = sendEmailDto.Body;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient(configuration[EmailConstants.SmtpClient] ?? "")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
            return true;
        } catch (Exception ex)
        {
            return false;
        }        
    }
}
