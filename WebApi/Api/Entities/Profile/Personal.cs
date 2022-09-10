using System.Text.Json.Serialization;

namespace Api.Entities.Profile
{
    public record Personal
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateofBirth { get; set; }
        public int AddressId { get; set; }
    }
}
