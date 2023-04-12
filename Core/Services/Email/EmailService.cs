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
            var from = new EmailAddress(emailuser.ReceiverEmail, emailuser.SenderName);
            var to = new EmailAddress(emailuser.ReceiverEmail, emailuser.ReceiverName);

            var htmlContent = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\r\n<title>Contact me</title>\r\n\r\n</head>\r\n \r\n<body bgcolor=\"#f6f6f6\">\r\n\r\n<!-- body -->\r\n<div>\r\n\t<h3>{from.Name}, is trying to reach you</h3>\r\n\t<p>{emailuser.Message}</p>\r\n\t<p>Thanks,<br><strong>{from.Name}</strong></p>\r\n</div>\r\n<!-- /body -->\r\n\r\n<!-- footer -->\r\n<div>\r\n\t<p>Sent from amitdole.com</p>\r\n</div>\r\n<!-- /footer -->\r\n\r\n</body>\r\n</html>";
          
            try
            {
                var message = await Task.Run(() => MailHelper.CreateSingleEmail(from, to, emailuser.Subject, null, htmlContent));
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