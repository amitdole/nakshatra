using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nakshatra.Api.Model.Profile;
using Nakshatra.Api.Model.Service;

namespace Nakshatra.PersonalWebsite.Web.Pages
{
    public class AboutModel : PageModel
    {
        private IUserProfileService _userProfileService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AboutModel> _logger;

        [BindProperty]
        public UserProfileInfo UserProfile { get; set; }

        public AboutModel(ILogger<AboutModel> logger, IUserProfileService userProfileService, IConfiguration configuration)
        {
            _logger = logger;
            _userProfileService = userProfileService;
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            try
            {
                var userProfile = _userProfileService.GetUserProfile(int.Parse(_configuration["PersonalWebSiteUserId"]));

                UserProfile = userProfile;
            }
            catch (Exception e)
            {
                _logger.LogError("Error on About", e);
            }

            return Page();
        }
    }
}