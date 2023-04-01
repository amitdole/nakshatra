using System.Text.Json.Serialization;

namespace Nakshatra.HostedServices.WebApi.Api.Entities.Common;

public record BaseEntity
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
}