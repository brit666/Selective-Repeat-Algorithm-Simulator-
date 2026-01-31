using System;

namespace Selective_Repeat_Simulator
{
    public class SimulationLink
    {
        public int PacketSizeBytes = 1024;
        public int WindowSize = 8;

        public double BandwidthMbps = 10;
        public double PropagationDelayMs = 50;
        public double LossRate = 0.05;

        private Receiver receiver; 
        private Sender sender;

        Random random = new Random();

        public SimulationLink(Receiver receiver, Sender sender)
        {
            this.receiver = receiver;
            this.sender = sender;
        }

        private bool IsLost()
        {
            return random.NextDouble() < LossRate;
        }

        private double TransmissionDelayMs()
        {
            double transmissionTime =
                (PacketSizeBytes * 8.0) / (BandwidthMbps * 1_000_000) * 1000;
            return transmissionTime + PropagationDelayMs;
        }

        public Packet SendPacket(Packet packet)
        {
            if (IsLost())
            {
                double delay = TransmissionDelayMs();
                Console.WriteLine($"Packet {packet.SeqNum} wasn't sent");
                return packet;
            }

            double totalDelay = TransmissionDelayMs();
            Console.WriteLine($"Packet {packet.SeqNum} was sent with delay {totalDelay} ms");
            packet = receiver.ReceiverReceivePacket(packet);

            return packet;
        }

    }
}
