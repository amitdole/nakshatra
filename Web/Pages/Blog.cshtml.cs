using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models;
using Services;

namespace AspnetRun.Web.Pages
{
    public class BlogModel : PageModel
    {
        private readonly ILogger<BlogModel> _logger;
        private readonly IConfiguration _configuration;
        private IProfileService _profileService;

        [BindProperty]
        public Posts Posts { get; set; }

        public BlogModel(ILogger<BlogModel> logger, IConfiguration configuration, IProfileService profileService)
        {
            _logger = logger;
            _configuration = configuration;
            _profileService = profileService;
        }

        public IActionResult OnGet(int? blog_page, string search)
        {
            var profile = _profileService.GetProfile(10001);
            var blogInfo = profile.BlogDetails.Where(b => b.Name.Equals("dotnetkari")).FirstOrDefault();
            var page = blog_page;
            // if on default page then page = 1
            if (blog_page == null || blog_page == 0)
            {
                page = 1;
            }

            var _bloggerService = new BloggerService();

            //Build Pagination
            var blogPages = BuildBloggerPagger(_bloggerService, search, blogInfo);

            //Get all posts
            Posts = _bloggerService.GetBlogs(blogInfo, blogPages[page ?? 1], search);

            //Pages * total post per page [10]
            Posts.TotalItems = blogPages.Count * 10;

            ViewData["BlogPage"] = page;
            return Page();
        }

        public void OnPost(Contact profile)
        {
        }

        private Dictionary<int, string> BuildBloggerPagger(BloggerService bloggerService, string search, BlogInfo blogInfo)
        {
            var page = 1;
            var blogPages = new Dictionary<int, string>();
            string? nextPageToken = null;

            //Default page does not have next token
#pragma warning disable CS8604 // Possible null reference argument.
            blogPages.Add(page, nextPageToken);
#pragma warning restore CS8604 // Possible null reference argument.

            //get 1st post page
            var Posts = bloggerService.GetBlogs(blogInfo, nextPageToken, search);

            //continue till last page is reached
            while (Posts.NextPageToken != null)
            {
                page++;

                //get post page
                Posts = bloggerService.GetBlogs(blogInfo, Posts.NextPageToken, search);

                //if page has next page then add NextPageToken to collection
                if (Posts.NextPageToken != null)
                {
                    blogPages.Add(page, Posts.NextPageToken);
                }
            }

            //WebUtils.Set(Response, "BlogPostPage", JsonConvert.SerializeObject(blogPages), 1200);

            return blogPages;
        }
    }
}