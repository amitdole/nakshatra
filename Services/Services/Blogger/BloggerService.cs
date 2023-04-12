using BenchmarkDotNet.Attributes;
using Nakshatra.Api.Model.Profile;
using Nakshatra.Services.Api.Model.Blog;
using Newtonsoft.Json;

namespace Services.Blogger;

[MemoryDiagnoser]
public class BloggerService : IBlogService
{
    public string Url { get; set; }
    public BloggerService(Blog blogConfiguration)
    {
        Url = $"{blogConfiguration.ApiUrl}/blogger/v3/blogs/" +
           $"{blogConfiguration.BlogServiceId}/" +
           $"posts?key={blogConfiguration.ApiKey}" +
           $"&maxResults={blogConfiguration.RetrivalCount}";
    }

    [Benchmark]
    public BlogDetails GetBlogs(string searchTerm)
    {
        BlogDetails blogDetails = null;

        //Get bloggers data based on the blog url & key
        //add search
        if (!string.IsNullOrEmpty(searchTerm))
        {
            Url = Url + $"&labels={searchTerm}";
        }

        using (var httpClient = new HttpClient())
        {
            using (var response = httpClient.GetAsync(Url))
            {
                string apiResponse = response.Result.Content.ReadAsStringAsync().Result;
                blogDetails = JsonConvert.DeserializeObject<BlogDetails>(apiResponse);
            }

            ////Get the 1st page of the blog
            //var postUrl = $"{blog.Posts.SelfLink}?key={blogInfo.Key}";

            ////if not 1st page, use nextPageToken to get next page
            //if (nextPageToken != null)
            //{
            //    postUrl = postUrl + $"&pageToken={nextPageToken}";
            //}

            //add search
            //if (!string.IsNullOrEmpty(searchTerm))
            //{
            //    postUrl = postUrl + $"&labels={searchTerm}";
            //}
            //using (var response = httpClient.GetAsync(postUrl))
            //{
            //    string apiResponse = response.Result.Content.ReadAsStringAsync().Result;

            //    try
            //    {
            //        post = JsonConvert.DeserializeObject<Post>(apiResponse);
            //    }
            //    catch
            //    {
            //        throw;
            //    }

            //}
        }
        return blogDetails;
    }

    public bool BlogExists()
    {
        using var httpClient = new HttpClient();
        using var response = httpClient.GetAsync(Url);
        string apiResponse = response.Result.Content.ReadAsStringAsync().Result;
        var blogDetails = JsonConvert.DeserializeObject<BlogDetails>(apiResponse);

        if (blogDetails != null && blogDetails.Items.Count > 0)
        {
            return true;
        }

        return false;
    }
}