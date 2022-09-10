using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using API.Model.Service;
using API.Model.Profile;

namespace AspnetRun.Web.Pages
{
    public class _ContactModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly ProfileInfo _profile;

        private IProfileService _profileService;

        [BindProperty]
        public ProfileInfo Profile { get; set; }

        public _ContactModel(IConfiguration config, ProfileInfo profile)
        {
            _profile = profile;
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        public IActionResult OnGet(ProfileInfo profile)
        {
            Profile = profile;

            return Page();
        }
    }
}