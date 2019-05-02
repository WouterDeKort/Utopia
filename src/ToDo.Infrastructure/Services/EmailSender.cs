using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace ToDo.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private AuthMessageSenderOptions options;
        private string apiKey;

        public EmailSender(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("utopia@example.com", "Utopia");
            var to = new EmailAddress(email);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            msg.SetClickTracking(false, false);

            await client.SendEmailAsync(msg);
        }
    }
}
