using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using BEx.ExchangeEngine.Utilities;

namespace BEx.ExchangeEngine
{
    public class SocketObserver : IObserver<string>
    {
        private readonly IMessageParser _parser;
        private readonly Queue<string> _messageQueue = new Queue<string>();
        private readonly object _sync = new object();
        
        internal SocketObserver(IMessageParser parser)
        {
            this._parser = parser;
        }


        public void OnNext(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _parser.Parse(value);
            }
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            
        }
    }
}