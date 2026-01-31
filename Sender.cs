using System;
using System.Collections.Generic;
using System.Linq;

namespace Selective_Repeat_Simulator
{
    public class Sender
    {
        private SimulationLink link;
        private Matrics stats = new Matrics();

        private Dictionary<int, Packet> window = new Dictionary<int, Packet>();

        private int baseSeq = 0;
        private int nextSeq = 0;

        private int TotalPackets = 1000;
        private double TimeoutMs = 120;
        private double CurrentTime = 0;
        double MaxSimTime = 100000;

        public Sender(SimulationLink link)
        {
            this.link = link;
        }

        public void Run()
        {
            while (stats.TotalAcked < TotalPackets && CurrentTime < MaxSimTime)
            {
                SendNewPackets();
                CheckTimeouts();

                CurrentTime += 1;
            }

            PrintResults();
        }

        private void SendNewPackets()
        {
            while (nextSeq < baseSeq + link.WindowSize && nextSeq < TotalPackets)
            {
                Packet p = new Packet
                {
                    SeqNum = nextSeq,
                    SendTime = CurrentTime
                };

                window[nextSeq] = p;
                stats.TotalSent++;

                Packet returned = link.SendPacket(p);

                if (returned.Acked)
                    ProcessAck(returned);

                nextSeq++;
            }
        }

        private void ProcessAck(Packet packet)
        {
            if (!window.ContainsKey(packet.SeqNum))
                return;

            if (!window[packet.SeqNum].Acked)
            {
                window[packet.SeqNum].Acked = true;
                stats.TotalAcked++;
                stats.TotalDelay += (CurrentTime - window[packet.SeqNum].SendTime);
            }

            while (window.ContainsKey(baseSeq) && window[baseSeq].Acked)
            {
                window.Remove(baseSeq);
                baseSeq++;
            }
        }

        private void CheckTimeouts()
        {
            foreach (var p in window.Values.ToList())
            {
                if (!p.Acked && (CurrentTime - p.SendTime) > TimeoutMs)
                {
                    Console.WriteLine($"Timeout for packet {p.SeqNum}, retransmitting...");
                    p.SendTime = CurrentTime;
                    stats.Retransmissions++;

                    Packet returned = link.SendPacket(p);

                    if (returned.Acked)
                        ProcessAck(returned);
                }
            }
        }

        private void PrintResults()
        {
            Console.WriteLine("\n--- Simulation Results ---");
            Console.WriteLine($"Total Sent: {stats.TotalSent}");
            Console.WriteLine($"Retransmissions: {stats.Retransmissions}");
            Console.WriteLine($"Average Delay: {link.PropagationDelayMs + (link.PacketSizeBytes * 8.0) / (link.BandwidthMbps * 1_000_000) * 1000} ms");
        }
    }
}
