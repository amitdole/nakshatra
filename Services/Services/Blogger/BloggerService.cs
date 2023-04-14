using BenchmarkDotNet.Attributes;
using Nakshatra.Services.Api.Model.Profile;
using Nakshatra.Services.Api.Model.Blog;
using Newtonsoft.Json;

namespace Nakshatra.Services.Blogger;

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
    public async Task<BlogDetails> GetBlogsAsync(string searchTerm = null)
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
                string apiResponse = await response.Result.Content.ReadAsStringAsync();
                blogDetails = JsonConvert.DeserializeObject<BlogDetails>(apiResponse);
            }
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