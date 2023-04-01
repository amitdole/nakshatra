using Nakshatra.Api.Model.Profile;
using Newtonsoft.Json;

namespace Services.Blogger;

public class BloggerService : IBloggerService
{
    public Post GetBlogs(BlogInfo blogInfo, string nextPageToken, string searchTerm)
    {
        Blog blog = null;
        var post = new Post();

        //Get bloggers data based on the blog url & key
        var googleBloggerApi = $"{blogInfo.Api}/blogger/v3/blogs/byurl?url={blogInfo.Url}&key={blogInfo.Key}";

        //add search
        if (!string.IsNullOrEmpty(searchTerm))
        {
            googleBloggerApi = googleBloggerApi + $"&labels={searchTerm}";
        }

        using (var httpClient = new HttpClient())
        {
            using (var response = httpClient.GetAsync(googleBloggerApi))
            {
                string apiResponse = response.Result.Content.ReadAsStringAsync().Result;
                blog = JsonConvert.DeserializeObject<Blog>(apiResponse);
            }

            //Get the 1st page of the blog
            var postUrl = $"{blog.Posts.SelfLink}?key={blogInfo.Key}";

            //if not 1st page, use nextPageToken to get next page
            if (nextPageToken != null)
            {
                postUrl = postUrl + $"&pageToken={nextPageToken}";
            }

            //add search
            if (!string.IsNullOrEmpty(searchTerm))
            {
                postUrl = postUrl + $"&labels={searchTerm}";
            }
            using (var response = httpClient.GetAsync(postUrl))
            {
                string apiResponse = response.Result.Content.ReadAsStringAsync().Result;

                try
                {
                    post = JsonConvert.DeserializeObject<Post>(apiResponse);
                }
                catch
                {
                    throw;
                }

            }
        }
        return post;
    }
}