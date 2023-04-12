using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Blogger;
using Nakshatra.Api.Model.Service;
using Nakshatra.Core.Services.Caching;
using Nakshatra.Api.Model.Profile;
using Nakshatra.Services.Api.Model.Blog;

namespace Nakshatra.PersonalWebsite.Web.Pages
{
    public class BlogModel : PageModel
    {
        private readonly ILogger<BlogModel> _logger;
        private readonly IConfiguration _configuration;
        private IUserProfileService _userProfileService;
        private readonly ICacheService _cacheService;
        private const string userProfileCacheKey = "user_profile_{0}";
        private const string blogCacheKey = "{0}_blog_{1}";

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public BlogDetails BlogDetails { get; set; }
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;


        [BindProperty]
        public UserProfileInfo Profile { get; set; }

        public BlogModel(ILogger<BlogModel> logger, IConfiguration configuration, IUserProfileService userProfileService, ICacheService cacheService)
        {
            _logger = logger;
            _configuration = configuration;
            _userProfileService = userProfileService;
            _cacheService = cacheService;
        }

        public IActionResult OnGet(string name, string searchTerms)
        {
            try
            {
                var blogItems = new List<BlogPost>();

                // Get User Profile
                var profileId = int.Parse(_configuration["PersonalWebSiteUserId"]);

                if (!_cacheService.TryGet(userProfileCacheKey, out UserProfileInfo userProfile))
                {
                    userProfile = _userProfileService.GetUserProfile(profileId);
                    _cacheService.Set(string.Format(userProfileCacheKey, profileId), userProfile);
                }

                Profile = userProfile;

                //Get Blog Configuration
                var blogConfiguration = Profile.BlogDetails.Where(b => b.Name.Equals(name)).FirstOrDefault();

                if (blogConfiguration == null)
                {
                    return NotFound();
                }

                var _bloggerService = new BloggerService(blogConfiguration);

                var blogPostsCacheKey = string.Format(blogCacheKey, blogConfiguration.Name, searchTerms);

                if (!_cacheService.TryGet(blogPostsCacheKey, out blogItems))
                {

                    blogItems = _bloggerService.GetBlogs(searchTerms).Items;
                    _cacheService.Set(blogPostsCacheKey, blogItems);
                }

                // Set BlogDetails and the Total Count
                var blogDetails = new BlogDetails
                {
                    BlogServiceId = blogConfiguration.BlogServiceId,
                    Name = blogConfiguration.Name,
                    ShortDescription = blogConfiguration.ShortDescription,
                    LongDescription = blogConfiguration.LongDescription,
                    Author = new Services.Api.Model.Blog.Author
                    {
                        DisplayName = blogConfiguration.AuthorName,
                        BlogDescription = blogConfiguration.AuthorBlogDescription,
                        Url = blogConfiguration.Url
                    },
                    Items = blogItems?.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList()
                };

                BlogDetails = blogDetails;
                Count = blogItems != null ? blogItems.Count : 0;
            }
            catch (Exception e)
            {
                _logger.LogError("Error on Blog", e);
            }

            return Page();
        }
    }
}