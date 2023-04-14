using Nakshatra.Services.Api.Model.Blog;

namespace Nakshatra.Services.Blogger;
public interface IBlogService
{
    bool BlogExists();
    Task<BlogDetails> GetBlogsAsync(string searchTerm);
}