using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nakshatra.Api.Model.Profile;
using Nakshatra.Api.Model.Service;

namespace AspnetRun.Web.Pages
{
    public class AboutModel : PageModel
    {
        private readonly ILogger<AboutModel> _logger;
        private IUserProfileService _userProfileService;

        [BindProperty]
        public UserProfileInfo UserProfile { get; set; }

        public AboutModel(ILogger<AboutModel> logger, IUserProfileService userProfileService)
        {
            _logger = logger;
            _userProfileService = userProfileService;
        }

        public IActionResult OnGet()
        {
            var userProfile = _userProfileService.GetUserProfile(10001);

            UserProfile = userProfile;

            return Page();
        }
    }
}