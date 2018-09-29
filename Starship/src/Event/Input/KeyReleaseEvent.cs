using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Redbus.Events;

namespace Starship.Event.Input
{
    public class KeyReleaseEvent : EventBase
    {
        public Keys Key { get; private set; }

        public KeyReleaseEvent(Keys key)
        {
            Key = key;
        }
    }
}
