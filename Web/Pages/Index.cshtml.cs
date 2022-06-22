using API.Model.Caching;
using API.Model.Profile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.CacheService;
using Services.Extensions;
using API.Model.Service;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IndexModel> _logger;
        private IProfileService _profileService;
        private readonly Func<CacheType, ICacheService> _cacheService;

        [BindProperty]
        public ProfileInfo Profile { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, IProfileService profileService, Func<CacheType, ICacheService> cacheService)
        {
            _logger = logger;
            _configuration = configuration;
            _profileService = profileService;
            _cacheService = cacheService;
        }

        public IActionResult OnGet()
        {
            var cacheKey = $"user_profile";
            var cacheProvider = _configuration["CacheProvider"].ToEnum<CacheType>();

            if (!_cacheService(cacheProvider).TryGet(cacheKey, out ProfileInfo profile))
            {
                profile = _profileService.GetProfile(int.Parse(_configuration["Profile:Id"]));
                _cacheService(cacheProvider).Set(cacheKey, profile);
            }

            Profile = profile;

            return Page();
        }

        public void OnPost(Web.Models.Contact profile)
        {
        }
    }
}