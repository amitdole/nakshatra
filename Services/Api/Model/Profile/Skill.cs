namespace Nakshatra.Services.Api.Model.Profile;

[Serializable]
public class Skill
{
    public int SkillId { get; set; }
    public string Name { get; set; }
    public string Technology { get; set; }
    public string Description { get; set; }
    public int Proficency { get; set; }
}