using System;

namespace Selective_Repeat_Simulator
{
    public class Receiver
    {
        public Packet ReceiverReceivePacket(Packet packet)
        {
            packet.Acked = true;

            return packet;
        }
    }
}
