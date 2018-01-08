using dermal.auth.Interfaces;
using dermal.auth.Options;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dermal.auth.Services
{
    public class EmailSender : IEmailSender
    {

        private readonly SendGridOptions _sendGridOptions;

        public EmailSender(IOptions<SendGridOptions> options)
        {
            _sendGridOptions = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(_sendGridOptions.SendGridKey);
            var fromEmail = new EmailAddress("test@example.com", "Example User");
            var toEmail = new EmailAddress(email, "Blake Mumford");
            var htmlContent = "<strong>Eat a massive chode.</strong>";
            var msg = MailHelper.CreateSingleEmail(fromEmail, toEmail, subject, null, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
