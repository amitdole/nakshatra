using Nakshatra.HostedServices.WebApi.Api.Entities.Reminders;

namespace Nakshatra.HostedServices.WebApi.Functions.WebApiFunctionApp;

public class ProcessReminders
{
    [FunctionName("ProcessReminders")]
    public static void Run([ServiceBusTrigger("reminders-queue-surya-test", Connection = "REMINDERS_QUEUE_SERVICE_BUS_TEST_CONNECTION_STRING")] string myQueueItem, ILogger log, ExecutionContext context)
    {
        var config = new ConfigurationBuilder()
        .SetBasePath(context.FunctionAppDirectory)
        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();
        var communicationServiceConnectionString = config["COMUNICATION_SERVICE_NAKSHTRA_TEST_CONNECTION_STRING"];

        var smsClient = new SmsClient(communicationServiceConnectionString);

        var reminderMessage = JsonConvert.DeserializeObject<ReminderMessage>(myQueueItem).Reminder;

        var reminder = $"Reminder: {reminderMessage.Type}, {reminderMessage.Name}, Due: {reminderMessage.DateCreated.ToString()}";

        var result = smsClient.Send(
             from: config["SEND_SMS_FROM"],
             to: config["SEND_SMS_TO"],
             message: reminder.Length > 160 ? reminder.Substring(0, 160) : reminder,
             options: new SmsSendOptions(enableDeliveryReport: true)
             {
                 Tag = "Reminders"
             });

        log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}, emaiilMessageId: {result.Value.MessageId}");
    }
}