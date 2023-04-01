using Nakshatra.Core.Api.Model.Email;
using SendGrid;

namespace Nakshatra.Core.Services.Email
{
    public interface IEmailService
    {
        Task<Response> Send(EmailInfo emailuser);
    }
}