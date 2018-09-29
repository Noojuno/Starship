using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Starship.Network.Packets;
using Starship.Util;

namespace Starship.Network
{
    //TODO: FIX PACKET HANDLER
    public static class PacketHandler
    {
        public static void HandlePacket(int id, NetIncomingMessage message)
        {
            switch (id)
            {
                case 0:
                    PacketTest pt = new PacketTest();
                    pt.FromMessage(message);
                    Logger.GetLogger().InfoFormat("Test: {0}", pt.GetTest());
                    break;
            }
        }
    }
}
