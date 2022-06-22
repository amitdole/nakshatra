namespace API.Model.Profile
{
    [Serializable]
    public class Blog
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public BlogPost Posts { get; set; }
    }
}
