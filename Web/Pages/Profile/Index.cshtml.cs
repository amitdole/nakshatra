using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Profile
{
    public class ProfileModel : PageModel
    {
        public Web.Models.Profile Profile { get; set; }


        public async Task OnGetAsync()
        {
            this.Profile = new Models.Profile
            {
                PersonalInfo = new Models.Personal
                {
                    FirstName = "Amit",
                    MiddleName = "M.",
                    LastName = "Dole"
                },

            };
            //Profile.PersonalInfo.FirstName = "Amit";
            //Profile.PersonalInfo.FirstName = "M";
            //Profile.PersonalInfo.FirstName = "Dole";

            //Profile.EducationInfos.Add(new Web.Models.Education { Course = "Bachaelor of Computer Science", StartYear = 2000, EndYear = 2003 });
            //Profile.EducationInfos.Add(new Web.Models.Education { Course = "Post Graduate Deploma in Business Administration", StartYear = 2005, EndYear = 2009 });
        }
    }
}