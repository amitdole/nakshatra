using Nakshatra.HostedServices.WebApi.Api.Entities.Reminders;

namespace Nakshatra.HostedServices.Services.Queues;

public interface IRemindersQueue: IBaseQueue<ReminderMessage>
{
}
