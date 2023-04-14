using Nakshatra.Services.Api.Model.Profile;

namespace Nakshatra.Api.Model.Profile;

[Serializable]
public class UserProfileInfo
{
    public string ProfileId { get; set; }
    public Personal PersonalDetails { get; set; }
    public Education[] EducationDetails { get; set; }
    public Certification[] CertificationDetails { get; set; }
    public Company[] CompanyDetails { get; set; }
    public Experience[] ExperienceDetails { get; set; }
    public Skill[] SkillDetails { get; set; }
    public Project[] ProjectDetails { get; set; }
    public WorkFlow[] WorkFlowDetails { get; set; }
    public Address[] AddressDetails { get; set; }
    public string[] ProfileDetails { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription1 { get; set; }
    public string LongDescription2 { get; set; }
    public Blog[] BlogDetails { get; set; }
    public Social[] SocialDetails { get; set; }
}
