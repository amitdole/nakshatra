namespace Nakshatra.Api.Model.Profile;

[Serializable]
public class Personal
{
    public int PersonalId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ReceiveEmail { get; set; }
    public string Phone { get; set; }
    public DateTime DateofBirth { get; set; }
    public int AddressId { get; set; }
}
