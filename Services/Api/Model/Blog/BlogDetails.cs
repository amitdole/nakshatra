namespace Nakshatra.Services.Api.Model.Blog;

[Serializable]
public class BlogDetails
{
    public string BlogServiceId { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public Author Author { get; set; }
    public List<BlogPost> Items { get; set; }
}
