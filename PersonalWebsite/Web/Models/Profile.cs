namespace Web.Models
{
    public class Profile
    {
        public Personal PersonalInfo { get; set; }
        public List<Education> EducationInfos { get; set; }
        public List<Work> WorkInfos { get; set; }

        public Contact ContactInfo { get; set; }
    }
}
