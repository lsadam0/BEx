using System;

namespace BEx.Response
{
    public class TickEventArgs : EventArgs
    {
        public TickEventArgs(Tick received)
        {
            Tick = received;
        }

        public Tick Tick { get; private set; }
    }
}