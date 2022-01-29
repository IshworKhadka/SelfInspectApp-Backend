using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HouseSelfInspection
{
    public class EmailSender : IEmailSender
    {

        readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string fromMail = _configuration["MailSettings:Mail"];
            string fromPassword = _configuration["MailSettings:Password"];

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body> " + htmlMessage + " </body></html>";
            message.IsBodyHtml = true;
            
            var smtpClient = new SmtpClient(_configuration["MailSettings:Host"])
            {
                Port = int.Parse(_configuration["MailSettings:Port"]),
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            await smtpClient.SendMailAsync(message);
        }
    }
}
