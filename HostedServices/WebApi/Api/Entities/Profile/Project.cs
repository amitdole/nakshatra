namespace Nakshatra.HostedServices.WebApi.Api.Entities.Profile;
[Serializable]
public class Project
{
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
}