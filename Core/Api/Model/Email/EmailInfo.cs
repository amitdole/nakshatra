namespace Nakshatra.Core.Api.Model.Email;

public class EmailInfo
{
    public string? SenderName { get; set; }
    public string? SenderEmail { get; set; }
    public string? ReceiverName { get; set; }
    public string? ReceiverEmail { get; set; }
    public string? Subject { get; set; }
    public string? Message { get; set; }
}
