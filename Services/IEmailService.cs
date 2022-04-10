using SendGrid;

namespace Services
{
    public interface IEmailService
    {
        Task<Response> Send(EmailInfo emailuser);
    }
}