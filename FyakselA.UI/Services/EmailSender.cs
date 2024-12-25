using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FyakselA.UI.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your Name", _configuration["EmailSettings:Username"]));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = htmlMessage };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"],
                                               int.Parse(_configuration["EmailSettings:Port"]),
                                               MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_configuration["EmailSettings:Username"],
                                                    _configuration["EmailSettings:Password"]);
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"Ошибка при отправке письма: {ex.Message}");
                    throw; 
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}