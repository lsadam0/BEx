using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace BEx.ExchangeEngine
{
    public class SocketObserver 
        : IObserver<string>
     
    {
        private readonly Queue<string> _messageQueue = new Queue<string>();
        private readonly IMessageParser _parser;
        private readonly object _sync = new object();
        private HashSet<object> _allObservers = new HashSet<object>();
    

        internal SocketObserver(IMessageParser parser)
        {
            _parser = parser;
          
        }


        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _parser.Parse(value);
            }
        }

        
    }
}