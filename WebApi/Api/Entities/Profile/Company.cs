using System.Text.Json.Serialization;

namespace Api.Entities.Profile
{
    public record Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
    }
}
