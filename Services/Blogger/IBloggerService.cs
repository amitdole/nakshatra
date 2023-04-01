using Nakshatra.Api.Model.Profile;

namespace Services.Blogger;

public interface IBloggerService
{
    Post GetBlogs(BlogInfo blogInfo, string nextPageToken, string searchTerm);
}