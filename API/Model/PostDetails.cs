namespace API.Model
{
    [Serializable]
    public class PostDetails
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Author Author { get; set; }
        public Replies Replies { get; set; }
        public string[] Labels { get; set; }
        public DateTime Published { get; set; }
    }
}
