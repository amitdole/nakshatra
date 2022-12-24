using Api.Entities.Reminders;

namespace Services.Services
{
    public interface IReminderService
    {
        Task<Reminder> AddReminderAsync(Reminder reminder);
        Task<List<Reminder>> ListAllRemindersAsync();
        Task DeleteReminderAsync(string reminderId);
    }
}