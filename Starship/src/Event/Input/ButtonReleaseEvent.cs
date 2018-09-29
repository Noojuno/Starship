using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Redbus.Events;

namespace Starship.Event.Input
{
    public class ButtonReleaseEvent : EventBase
    {
        public Buttons Button { get; private set; }

        public ButtonReleaseEvent(Buttons button)
        {
            Button = button;
        }
    }
}
