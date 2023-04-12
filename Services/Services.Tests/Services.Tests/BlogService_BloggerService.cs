using FluentAssertions;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nakshatra.Api.Model.Service;
using Services.Blogger;
using Services.Profile;
using Xunit;

namespace Nakshatra.HostedServices.Services.Tests
{
    public class BlogService_BloggerService: IClassFixture<TestBase>
    {
        private readonly IHost _host;
        public BlogService_BloggerService(TestBase testBase)
        {
            _host = testBase.TestHost;
        }

        [Fact]
        public void BloggerBlogExists()
        {
            //Arange
            var blogName = "dotnetkari";
            var userProfileService = _host.Services.GetService<IUserProfileService>();

            // Get User Profile
            var profileId = 10001;

            
            var userProfile = userProfileService.GetUserProfile(profileId);

            //Get Blog Configuration
            var blogConfiguration = userProfile.BlogDetails.Where(b => b.Name.Equals(blogName)).FirstOrDefault();


            //Act
            blogConfiguration.IsNull().Should().BeFalse();

            var blogService = new BloggerService(blogConfiguration);

            var response = blogService.BlogExists();

            //Assert
            response.Should().BeTrue();
        }
    }
}