using Api.Entities.Reminders;
using Azure.Messaging.ServiceBus;
using Services.Queues;

namespace Services.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IRemindersQueue _remindersQueue;
        public ReminderService(IRemindersQueue remindersQueue)
        {
            _remindersQueue = remindersQueue;
        }
        public async Task<Reminder> AddReminderAsync(Reminder reminder)
        {
            var result = _remindersQueue.SendMessageAsync(new ReminderMessage
            {
                Reminder = reminder,
                Action = "Add"
            });

            return result.Result.Reminder;
        }

        public Task DeleteReminderAsync(string reminderId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Reminder>> ListAllRemindersAsync()
        {
            var result = _remindersQueue.ReceiveMessagesAsync();
            var reminders = new List<Reminder>();

            foreach (var reminderMessage in result.Result)
            {
                reminders.Add(reminderMessage.Reminder);
            }
            return reminders;
        }
    }
}
