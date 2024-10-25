using PageManagementSystem.Application.Interfaces;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace PageManagementSystem.Application.Services
{
    public class EmailSender : IEmailSenderService
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string host = _configuration["Smtp:Host"];
            int port = Convert.ToInt32(_configuration["Smtp:Port"]);
            string _username = _configuration["Smtp:UserName"];
            string _password = _configuration["Smtp:Password"];
            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(_username, _password),
                EnableSsl = true
            };
            return client.SendMailAsync(new MailMessage(_username, email, subject, htmlMessage) { IsBodyHtml = true });
        }
    }
}
