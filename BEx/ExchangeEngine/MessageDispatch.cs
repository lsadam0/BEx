using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using BEx.ExchangeEngine.Utilities;

namespace BEx.ExchangeEngine
{
    public class MessageDispatch : IDisposable, IObserver<string>
    {
        private IMessageParser _parser;
        private Task _dispatchTask;
        private readonly Queue<string> _messageQueue = new Queue<string>();

        private bool _pleaseStop;
        private readonly object _sync = new object();
        
        internal MessageDispatch(IMessageParser parser)
        {
            this._parser = parser;
            /*
            _dispatchTask = Task.Run(() =>
            {
                Dispatch();
            });*/
        }

        public void Dispose()
        {
            if (_dispatchTask != null)
            {
                _pleaseStop = true;

                lock (_sync)
                {
                    Monitor.Pulse(_sync);
                }

                _dispatchTask = null;
            }
        }

        public void OnNext(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                // value.Log();
                _parser.Parse(value);
            }
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void EnqueueMessage(string message)
        {
            lock (_sync)
            {
                _messageQueue.Enqueue(message);

                Monitor.Pulse(_sync);
            }
        }

        private void Dispatch()
        {
            var buffer = string.Empty;

            while (!_pleaseStop)
            {
                try
                {
                    lock (_sync)
                    {
                        if (_messageQueue.Count == 0)
                        {
                            Monitor.Wait(_sync);
                        }
                        else

                        {
                            buffer = _messageQueue.Dequeue();
                        }
                    }

                    if (!string.IsNullOrEmpty(buffer))
                    {
                        //  var translated = Parse(buffer);
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError("Dispatch: {0}", e);
                }
            }
        }

        /*
        internal abstract IExchangeResult Parse(string message);

        internal */
    }
}