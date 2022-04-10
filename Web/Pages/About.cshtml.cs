using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRun.Web.Pages
{
    public class AboutModel : PageModel
    {
        private readonly ILogger<AboutModel> _logger;
        private IProfileService _profileService;

        [BindProperty]
        public API.Model.Profile Profile { get; set; }

        public AboutModel(ILogger<AboutModel> logger, IProfileService profileService)
        {
            _logger = logger;
            _profileService = profileService;
        }

        public IActionResult OnGet()
        {
            var profile = _profileService.GetProfile(10001);

            Profile = profile;

            return Page();
        }
    }
}