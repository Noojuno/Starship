using Microsoft.Xna.Framework;
using Redbus.Events;

namespace Starship.Event
{
    public class ResizeEvent : EventBase
    {
        public Rectangle Size;

        public ResizeEvent(Rectangle size)
        {
            this.Size = size;
        }
    }
}
