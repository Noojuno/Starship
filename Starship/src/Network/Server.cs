using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Lidgren.Network;
using Starship.Network.Packets;
using Starship.Util;

namespace Starship.Network
{
    public class Server
    {
        private NetPeerConfiguration config = new NetPeerConfiguration("starship");
        private NetServer server;
        private Task receiveTask;
        private CancellationTokenSource recieveStopToken = new CancellationTokenSource();
        private ILog logger;

        public Server()
        {
            logger = Logger.GetLogger(this.GetType());

            config.Port = 63367;
            //config.AcceptIncomingConnections = true;
            config.MaximumConnections = 100;
            //config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);

            receiveTask = new Task(() => ReceiveMessage(), recieveStopToken.Token, TaskCreationOptions.LongRunning);

        }

        public Server(int port)
        {
            config.Port = port;
        }

        public void Start()
        {
            server = new NetServer(config);
            server.Start();
            receiveTask.Start();

            logger.InfoFormat("Server started on port {0}:{1}", config.LocalAddress, server.Port);

        }

        public void Stop()
        {
            server.Shutdown("Shutdown");
            receiveTask.Dispose();
        }

        public void SendPacket(IPacket packet, NetClient client)
        {
            var message = server.CreateMessage();
            message.Write((byte)packet.GetId());
            packet.ToMessage(message);

            server.SendMessage(message, client.ServerConnection, NetDeliveryMethod.ReliableSequenced, 0);
        }

        public void SendAll(IPacket packet)
        {
            Console.WriteLine(server.Connections[0]);

            var message = server.CreateMessage();
            message.Write((byte)packet.GetId());
            packet.ToMessage(message);

            server.SendToAll(message, NetDeliveryMethod.ReliableSequenced);
        }

        public void ReceiveMessage()
        {
            while (!recieveStopToken.Token.IsCancellationRequested)
            {
                NetIncomingMessage msgIn;
                while ((msgIn = server.ReadMessage()) != null)
                {
                    //create message type handling with a switch
                    switch (msgIn.MessageType)
                    {
                        case NetIncomingMessageType.Data:
                            PacketHandler.HandlePacket(msgIn.ReadInt32(), msgIn);
                            break;
                        //All other types are for library related events (some examples)
                        case NetIncomingMessageType.DiscoveryRequest:
                            NetOutgoingMessage msg = server.CreateMessage();
                            //add a string as welcome text
                            msg.Write("Hellooooo Client");
                            //send a response
                            server.SendDiscoveryResponse(msg, msgIn.SenderEndPoint);
                            break;
                        case NetIncomingMessageType.ConnectionApproval:
                            msgIn.SenderConnection.Approve();
                            break;
                        case NetIncomingMessageType.StatusChanged:
                            NetConnectionStatus status = (NetConnectionStatus)msgIn.ReadByte();

                            logger.Info(status);
                            break;
                        default:
                            Console.WriteLine("Unhandled type: " + msgIn.MessageType + " " + msgIn.LengthBytes + " bytes");
                            break;

                    }
                    //Recycle the message to create less garbage
                    server.Recycle(msgIn);
                    recieveStopToken.Token.WaitHandle.WaitOne(1);
                }
            }
        }
    }
}
