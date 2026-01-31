using System;

namespace Selective_Repeat_Simulator
{
    public class Packet
    {
        public int SeqNum;
        public double SendTime;
        public bool Acked = false;
    }
}
