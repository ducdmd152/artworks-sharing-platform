using ArtHubBO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubService.Interface;

public interface IEmailService
{
    bool SendEmail(SendEmailDto sendEmailDto);
}
