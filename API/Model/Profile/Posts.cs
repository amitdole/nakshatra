namespace API.Model.Profile
{
    [Serializable]
    public class Posts
    {
        public string NextPageToken { get; set; }
        public int TotalItems { get; set; }
        public PostDetails[] Items { get; set; }
    }
}
