namespace API.Model
{
    [Serializable]
    public class Profile
    {
        public int Id { get; set; }
        public Personal PersonalDetails { get; set; }
        public Education[] EducationDetails { get; set; }
        public Company[] CompanyDetails { get; set; }
        public Experience[] ExperienceDetails { get; set; }
        public Skill[] SkillDetails { get; set; }
        public WorkFlow[] WorkFlowDetails { get; set; }
        public Address[] AddressDetails { get; set; }
        public string[] ProfileDetails { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription1 { get; set; }
        public string LongDescription2 { get; set; }
        public BlogInfo[] BlogDetails { get; set; }
    }
}
