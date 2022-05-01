using System.Text.Json.Serialization;

namespace Api.Entities.Profile
{
    public record Address
    {
        public int AddressId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
