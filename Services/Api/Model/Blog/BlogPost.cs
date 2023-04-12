namespace Nakshatra.Services.Api.Model.Blog;

[Serializable]
public class BlogPost
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Published { get; set; }
    public List<string> Labels { get; set; }
    public Comment Replies { get; set; }
}