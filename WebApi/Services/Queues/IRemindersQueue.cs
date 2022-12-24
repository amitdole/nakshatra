using Api.Entities.Reminders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Queues
{
    public interface IRemindersQueue: IBaseQueue<ReminderMessage>
    {
    }
}
