namespace Nakshatra.HostedServices.WebApi.Api.Entities.Profile;

public record Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int AddressId { get; set; }
}
