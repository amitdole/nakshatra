using System.ComponentModel.DataAnnotations;

namespace Nakshatra.PersonalWebsite.Web.Models
{
    public class Contact
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
