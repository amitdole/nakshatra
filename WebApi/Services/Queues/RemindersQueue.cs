using Api.Entities.Reminders;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Services.Queues
{
    public class RemindersQueue : IRemindersQueue
    {
        ServiceBusClient _serviceBusClient;
        ServiceBusSender _serviceBusSender;
        ServiceBusReceiver _serviceBusReceiver;
        public RemindersQueue(IConfiguration config)
        {
            var queueName = config["RemindersQueueName"];
            _serviceBusClient = new ServiceBusClient(config["reminders-queue-service-bus-connectionstring"]);
            _serviceBusSender = _serviceBusClient.CreateSender(queueName);
            _serviceBusReceiver = _serviceBusClient.CreateReceiver(queueName, new ServiceBusReceiverOptions()
            {
                ReceiveMode = ServiceBusReceiveMode.PeekLock
            });
        }

        public async Task<List<ReminderMessage>> ReceiveMessagesAsync()
        {
            var reminderMessages = new List<ReminderMessage>();
            var messages = _serviceBusReceiver.ReceiveMessagesAsync();

            await foreach (var message in messages)
            {
                var reminderMessage = JsonConvert.DeserializeObject<ReminderMessage>(message.Body.ToString());

                if (reminderMessage != null)
                {
                    reminderMessages.Add(reminderMessage);
                }
            }

            return reminderMessages;
        }

        public async Task<ReminderMessage> SendMessageAsync(ReminderMessage message)
        {
            await _serviceBusSender.SendMessageAsync(new ServiceBusMessage(JsonConvert.SerializeObject(message)));

            return message;
        }
    }
}
