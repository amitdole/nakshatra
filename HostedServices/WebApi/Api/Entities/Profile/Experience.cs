namespace Nakshatra.HostedServices.WebApi.Api.Entities.Profile;
public record Experience
{
    public int ExperienceId { get; set; }
    public string Role { get; set; }
    public int CompanyId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
