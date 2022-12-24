using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Entities.Reminders;

namespace Api.Entities.Reminders
{
    public class ReminderMessage
    {
        public Reminder Reminder { get; set; }

        public string Action { get; set; }
    }
}
