using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redbus.Events;

namespace Starship.Event
{
    public class InitializationEvent : EventBase
    {
        public string Status;

        public InitializationEvent(string status)
        {
            Status = status;
        }
    }
}
