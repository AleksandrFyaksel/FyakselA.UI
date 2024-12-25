using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace FyakselA.UI.Services
{
    public class FakeEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            
            Console.WriteLine($"Письмо отправлено на {email} с темой '{subject}'.");
            return Task.CompletedTask; 
        }
    }
}