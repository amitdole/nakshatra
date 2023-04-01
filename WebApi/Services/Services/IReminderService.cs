using Nakshatra.HostedServices.WebApi.Api.Entities.Reminders;

namespace Nakshatra.HostedServices.Services.Services;

public interface IReminderService
{
    Task<Reminder> AddReminderAsync(Reminder reminder);
    Task<List<Reminder>> ListAllRemindersAsync();
    Task DeleteReminderAsync(string reminderId);
}