using System.Text.Json.Serialization;

namespace Api.Entities.Common
{
    public record BaseEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}