﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redbus.Events;

namespace Starship.Event.Input
{
    public class PostInitializationEvent : EventBase
    {
        public string Status { get; private set; }

        public PostInitializationEvent(string status)
        {
            Status = status;
        }
    }
}
