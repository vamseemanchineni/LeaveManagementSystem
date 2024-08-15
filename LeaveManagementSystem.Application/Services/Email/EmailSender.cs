﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace LeaveManagementSystem.Application.Services.Email
{
    public class EmailSender(IConfiguration _configuration) : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fromAddress = _configuration["EmailSettings:DefaultEmailaddress"];
            var smtpServer = _configuration["EmailSettings:Server"];
            var smtpPort = Convert.ToInt32(_configuration["EmailSettings:Port"]);
            var message = new MailMessage
            {
                From = new MailAddress(fromAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(email));
            using var client = new SmtpClient(smtpServer, smtpPort);
            await client.SendMailAsync(message);
            // throw new NotImplementedException();
        }
    }
}
