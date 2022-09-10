using API.Model.Profile;

namespace Services.Profile
{
    public interface IBloggerService
    {
        //Task<Posts> GetBlogs(string key, string nextPageToken);
        Posts GetBlogs(BlogInfo blogInfo, string nextPageToken, string searchTerm);

    }
}