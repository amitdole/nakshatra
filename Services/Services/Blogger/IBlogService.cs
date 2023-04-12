using Nakshatra.Services.Api.Model.Blog;

namespace Services.Blogger;
public interface IBlogService
{
    bool BlogExists();
    BlogDetails GetBlogs(string searchTerm);
}