namespace Nakshatra.Api.Model.Profile;

[Serializable]
public class Blog
{
    public int BlogId { get; set; }
    public string BlogServiceId { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public string AuthorName { get; set; }
    public string AuthorBlogDescription { get; set; }
    public string Url { get; set; }
    public int RetrivalCount { get; set; }
    public string ApiUrl { get; set; }
    public string ApiKey { get; set; }
}