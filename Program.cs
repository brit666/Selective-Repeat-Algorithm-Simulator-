using System;

namespace Selective_Repeat_Simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Receiver r = new Receiver();
            Sender s = null;
            SimulationLink link = new SimulationLink(r, s);
            s = new Sender(link);

            link = new SimulationLink(r, s);

            s.Run();
        }
    }
}
