using System.Text.Json.Serialization;

namespace Api.Entities.Profile
{
    public record BlogInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Api { get; set; }
        public string Url { get; set; }
        public string Key { get; set; }
    }
}
