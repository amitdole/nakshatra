using Api.Entities.Common;

namespace Api.Entities.Reminders
{
    public record Reminder : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateDue { get; set; }
        private string Status { get; set; }
    }
}
