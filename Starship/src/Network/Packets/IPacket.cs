using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Starship.Network.Packets
{
    public interface IPacket
    {
        int GetId();

        void FromMessage(NetIncomingMessage message);

        void ToMessage(NetOutgoingMessage message);

        //IPacket Create();
    }
}
