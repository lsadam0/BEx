using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;

namespace BEx.Exchanges.Bitfinex.WebSocket
{
    internal class Parser : IMessageParser
    {
        public object Parse(string message)
        {
            message.Log();

            return null;
        }
    }
}