using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using Nakshatra.Core.Services.Email;
using Nakshatra.Core.Api.Model.Email;

namespace Nakshatra.Services.Profile
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Response> Send(EmailInfo emailuser)
        {
            var apiKey = _configuration["SendGridAPISecretKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(emailuser.Sender);
            var to = new EmailAddress(emailuser.Receiver);

            var htmlContent = "";
            var textContent = $"{emailuser.Message}";

            try
            {
                var message = await Task.Run(() => MailHelper.CreateSingleEmail(from, to, emailuser.Subject, textContent, htmlContent));
                var response = await client.SendEmailAsync(message);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}