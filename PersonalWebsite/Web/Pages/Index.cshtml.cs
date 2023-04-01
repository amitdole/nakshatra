using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nakshatra.Api.Model.Profile;
using Nakshatra.Api.Model.Service;
using Nakshatra.Core.Services.Caching;
using Services.Profile;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IndexModel> _logger;
        private IUserProfileService _userProfileService;
        private readonly ICacheService _cacheService;

        [BindProperty]
        public UserProfileInfo UserProfile { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, IUserProfileService userProfileService, ICacheService cacheService)
        {
            _logger = logger;
            _configuration = configuration;
            _userProfileService = userProfileService;
            _cacheService = cacheService;
        }

        public IActionResult OnGet()
        {
            var cacheKey = $"user_profile";

            if (!_cacheService.TryGet(cacheKey, out UserProfileInfo userProfile))
            {
                userProfile = _userProfileService.GetUserProfile(int.Parse(_configuration["Profile:Id"]));
                _cacheService.Set(cacheKey, userProfile);
            }

            UserProfile = userProfile;

            return Page();
        }

        public void OnPost(Web.Models.Contact profile)
        {
        }
    }
}