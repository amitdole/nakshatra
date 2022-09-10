using System.Text.Json.Serialization;

namespace Api.Entities.Profile
{
    public record Education
    {
        public int Id { get; set; }
        public string Qualification { get; set; }
        public string College { get; set; }
        public int AddressId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
