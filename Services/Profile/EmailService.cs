using API.Model.Profile;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Services.Profile
{
    public class EmailService : IEmailService
    {
        private readonly Configuration _configuration;
        public EmailService(Configuration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Response> Send(EmailInfo emailuser)
        {
            var apiKey = _configuration.Metadata["SendGridAPISecretKey"];
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