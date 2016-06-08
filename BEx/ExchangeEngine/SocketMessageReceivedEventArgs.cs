using System;

namespace BEx.ExchangeEngine
{
    internal class SocketMessageReceivedEventArgs : EventArgs
    {
        public SocketMessageReceivedEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}