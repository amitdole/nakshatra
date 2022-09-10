using System.Text.Json.Serialization;

namespace Api.Entities.Profile
{
    public record WorkFlow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
