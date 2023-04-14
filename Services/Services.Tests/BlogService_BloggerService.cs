using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Nakshatra.Services.Api.Model.Profile;
using Nakshatra.HostedServices.WebApi.Services.Services;
using Nakshatra.Services.Blogger;
using Xunit;

namespace Nakshatra.Services.Tests
{
    public class BlogService_BloggerService : IClassFixture<TestBase>
    {
        private ServiceProvider _serviceProvider;
        private IProfileService? _profileService;
        public BlogService_BloggerService(TestBase testBase)
        {
            _serviceProvider = testBase.ServiceProvider;
            _profileService = _serviceProvider.GetService<IProfileService>();
        }

        [Fact]
        public void BloggerBlogExists()
        {
            //Arange
            var blogName = "dotnetkari";

            // Get User Profile
            var profileId = "10001";


            var userProfile = _profileService?.GetProfile(profileId).Result;

            //Get Blog Configuration
            var blogConfiguration = userProfile?.BlogDetails.Where(b => b.Name.Equals(blogName)).FirstOrDefault();


            //Act
            blogConfiguration.Should().NotBeNull();

            var blogService = new BloggerService(new Blog
            {
                ApiKey = blogConfiguration?.ApiKey,
                ApiUrl = blogConfiguration?.ApiUrl,
                AuthorBlogDescription = blogConfiguration?.AuthorBlogDescription,
                AuthorName = blogConfiguration?.AuthorName,
                BlogId = blogConfiguration.BlogId,
                BlogServiceId = blogConfiguration?.BlogServiceId,
                LongDescription = blogConfiguration?.LongDescription,
                Name = blogConfiguration?.Name,
                RetrivalCount = blogConfiguration.RetrivalCount,
                ShortDescription = blogConfiguration?.ShortDescription,
                Url = blogConfiguration?.Url,

            });

            var response = blogService.BlogExists();

            //Assert
            response.Should().BeTrue();
        }

        [Fact]
        public void RetreiveBloggerBlogs()
        {
            //Arange
            var blogName = "dotnetkari";

            // Get User Profile
            var profileId = "10001";
            var blogCount = 100;


            var userProfile = _profileService?.GetProfile(profileId).Result;

            //Get Blog Configuration
            var blogConfiguration = userProfile?.BlogDetails.Where(b => b.Name.Equals(blogName)).FirstOrDefault();


            //Act
            blogConfiguration.Should().NotBeNull();

            var blogService = new BloggerService(new Blog
            {
                ApiKey = blogConfiguration?.ApiKey,
                ApiUrl = blogConfiguration?.ApiUrl,
                AuthorBlogDescription = blogConfiguration?.AuthorBlogDescription,
                AuthorName = blogConfiguration?.AuthorName,
                BlogId = blogConfiguration.BlogId,
                BlogServiceId = blogConfiguration?.BlogServiceId,
                LongDescription = blogConfiguration?.LongDescription,
                Name = blogConfiguration?.Name,
                RetrivalCount = blogConfiguration.RetrivalCount,
                ShortDescription = blogConfiguration?.ShortDescription,
                Url = blogConfiguration?.Url,

            });

            var response = blogService.GetBlogsAsync().Result;

            //Assert
            response.Items.Should().NotBeNull();

            response.Items.Count().Should().BeGreaterThan(blogCount);
        }

        [Fact]
        public void RetreiveTop10BloggerBlogs()
        {
            //Arange
            var blogName = "dotnetkari";

            // Get User Profile
            var profileId = "10001";
            var blogCount = 10;


            var userProfile = _profileService?.GetProfile(profileId).Result;

            //Get Blog Configuration
            var blogConfiguration = userProfile?.BlogDetails.Where(b => b.Name.Equals(blogName)).FirstOrDefault();


            //Act
            blogConfiguration.Should().NotBeNull();

            var blogService = new BloggerService(new Blog
            {
                ApiKey = blogConfiguration?.ApiKey,
                ApiUrl = blogConfiguration?.ApiUrl,
                AuthorBlogDescription = blogConfiguration?.AuthorBlogDescription,
                AuthorName = blogConfiguration?.AuthorName,
                BlogId = blogConfiguration.BlogId,
                BlogServiceId = blogConfiguration?.BlogServiceId,
                LongDescription = blogConfiguration?.LongDescription,
                Name = blogConfiguration?.Name,
                RetrivalCount = blogConfiguration.RetrivalCount,
                ShortDescription = blogConfiguration?.ShortDescription,
                Url = blogConfiguration?.Url,

            });

            var response = blogService.GetBlogsAsync().Result;

            //Assert
            response.Items.Should().NotBeNull();

            response.Items.Count().Should().BeGreaterThan(blogCount);
        }
    }
}