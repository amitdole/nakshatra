using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nakshatra.Api.Model.Profile;

namespace AspnetRun.Web.Pages
{
    public class _ContactModel : PageModel
    {
        [BindProperty]
        public UserProfileInfo UserProfile { get; set; }

        public _ContactModel()
        {
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        public IActionResult OnGet(UserProfileInfo userProfile)
        {
            UserProfile = userProfile;

            return Page();
        }
    }
}