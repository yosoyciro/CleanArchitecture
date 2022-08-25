using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace CleanArchitecture.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;
        private readonly ILogger<EmailService> logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            this.emailSettings = emailSettings.Value;
            this.logger = logger;
        }
        public async Task<bool> SendEmail(Application.Models.Email eMail)
        {
            var client = new SendGridClient(emailSettings.ApiKey);

            var subject = eMail.Subject;
            var to = new EmailAddress(eMail.To);
            var emailBody = eMail.Body;

            var from = new EmailAddress
            {
                Email = emailSettings.FromAddress,
                Name = emailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var ret = await client.SendEmailAsync(sendGridMessage);

            if (ret.StatusCode == HttpStatusCode.Accepted || ret.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            logger.LogError("Error al enviar el correo");
            return false;
        }
    }
}
