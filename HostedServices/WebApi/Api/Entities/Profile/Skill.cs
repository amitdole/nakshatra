namespace Nakshatra.HostedServices.WebApi.Api.Entities.Profile;
public record Skill
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Technology { get; set; }
    public string Description { get; set; }
    public int Proficency { get; set; }
}
