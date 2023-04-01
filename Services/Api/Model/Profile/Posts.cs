namespace Nakshatra.Api.Model.Profile;

[Serializable]
public class Post
{
    public string NextPageToken { get; set; }
    public int TotalItems { get; set; }
    public PostDetails[] Items { get; set; }
}
