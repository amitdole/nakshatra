namespace Nakshatra.HostedServices.WebApi.Api.Entities.Profile;

public record Address
{
    public int Id { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
}
