using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Starship.Util;

namespace Starship.Network.Packets
{
    public struct PacketTest : IPacket
    {
        private const int id = 0;
        private int test;

        public PacketTest(int a = 88)
        {
            test = a;
        }

        public int GetId()
        {
            return id;
        }

        public int GetTest()
        {
            return test;
        }

        public void FromMessage(NetIncomingMessage message)
        {
            this.test = message.ReadInt32();
        }

        public void ToMessage(NetOutgoingMessage message)
        {
            message.Write(this.test);
        }
    }
}
