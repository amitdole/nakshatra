using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nakshatra.Api.Model.Profile;
using Nakshatra.Api.Model.Service;
using Nakshatra.Core.Services.Caching;
using Nakshatra.Services.Api.Model.Blog;
using Nakshatra.Services.Blogger;

namespace Nakshatra.PersonalWebsite.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IndexModel> _logger;
        private IUserProfileService _userProfileService;
        private readonly ICacheService _cacheService;
        private const string userProfileCacheKey = "user_profile_{0}";
        private const string blogCacheKey = "{0}_blog_{1}";

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
            var profileId = int.Parse(_configuration["PersonalWebSiteUserId"]);

            try
            {
                if (!_cacheService.TryGet(string.Format(userProfileCacheKey, profileId), out UserProfileInfo userProfile))
                {
                    userProfile = _userProfileService.GetUserProfile(profileId);
                    _cacheService.Set(string.Format(userProfileCacheKey, profileId), userProfile);
                }

                GetBlogs(userProfile, _cacheService);

                UserProfile = userProfile;
            }
            catch (Exception e)
            {
                _logger.LogError("Error on Contact", e);

                throw;
            }

            return Page();
        }

        private static void GetBlogs(UserProfileInfo userProfile, ICacheService cacheService)
        {
            var blogName = "dotnetkari";
            var blogItems = new List<BlogPost>();

            Task.Run(async () =>
            {
                //Get Blog Configuration
                var blogConfiguration = userProfile.BlogDetails.Where(b => b.Name.Equals(blogName)).FirstOrDefault();

                if (blogConfiguration != null)
                {
                    var _bloggerService = new BloggerService(blogConfiguration);

                    var blogPostsCacheKey = string.Format(blogCacheKey, blogConfiguration.Name, "");

                    if (!cacheService.TryGet(blogPostsCacheKey, out blogItems))
                    {
                        var blogDetails = await _bloggerService.GetBlogsAsync();
                        cacheService.Set(blogPostsCacheKey, blogDetails.Items);
                    }
                }
            });
        }
    }
}