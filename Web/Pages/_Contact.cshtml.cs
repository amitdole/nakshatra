using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Web.Models;

namespace AspnetRun.Web.Pages
{
    public class _ContactModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly API.Model.Profile _profile;

        private IProfileService _profileService;

        [BindProperty]
        public API.Model.Profile Profile { get; set; }

        public _ContactModel(IConfiguration config, API.Model.Profile profile)
        {
            _profile = profile;
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        public IActionResult OnGet(API.Model.Profile profile)
        {
            Profile = profile;

            return Page();
        }
    }
}