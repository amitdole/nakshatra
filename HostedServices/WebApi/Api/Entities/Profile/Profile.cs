using Nakshatra.HostedServices.WebApi.Api.Entities.Common;
using System.Text.Json;

namespace Nakshatra.HostedServices.WebApi.Api.Entities.Profile;

public record Profile : BaseEntity
{
    public string ProfileId { get; set; }
    public Personal PersonalDetails { get; set; }
    public ICollection<Education> EducationDetails { get; set; }
    public ICollection<Certification> CertificationDetails { get; set; }
    public ICollection<Company> CompanyDetails { get; set; }
    public ICollection<Experience> ExperienceDetails { get; set; }
    public ICollection<Skill> SkillDetails { get; set; }
    public ICollection<Project> ProjectDetails { get; set; }
    public ICollection<WorkFlow> WorkFlowDetails { get; set; }
    public ICollection<Address> AddressDetails { get; set; }
    public string[] ProfileDetails { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription1 { get; set; }
    public string LongDescription2 { get; set; }
    public ICollection<Blog> BlogDetails { get; set; }
    public ICollection<Social> SocialDetails { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
