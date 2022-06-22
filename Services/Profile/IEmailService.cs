using SendGrid;

namespace Services.Profile
{
    public interface IEmailService
    {
        Task<Response> Send(EmailInfo emailuser);
    }
}