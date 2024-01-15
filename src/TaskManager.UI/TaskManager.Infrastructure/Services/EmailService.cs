using Microsoft.Extensions.Options;
using TaskManager.Infrastructure.Contracts;
using TaskManager.Infrastructure.Models;
using System.Net.Mail;
using System.Net;

namespace TaskManager.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using (var client = new SmtpClient(this.emailSettings.SmtpServer, this.emailSettings.SmtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(this.emailSettings.SmtpUsername, this.emailSettings.SmtpPassword);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(this.emailSettings.FromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
