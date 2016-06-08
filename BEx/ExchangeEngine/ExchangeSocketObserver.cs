using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine
{
    public class ExchangeSocketObserver : IDisposable
    {
        private readonly int _chunkSize = 1024;
        private MessageDispatch _dispatch = new MessageDispatch(new GdaxParser());
        private readonly UTF8Encoding _encoding = new UTF8Encoding();

        private int _isShutDownRequested;

        private IObservable<string> _observable;

        // private Task _observer;
        private readonly HashSet<IObserver<string>> _observers = new HashSet<IObserver<string>>();

        private readonly Uri _socketUri;
        // private CancellationToken _token;
        // private CancellationTokenSource _tokenSource;

        public ExchangeSocketObserver(IExchangeConfiguration configuration, object subscribe)
        {
            _socketUri = configuration.WebSocketUri;

            _observable = MessageBuffer(subscribe);
        }

        public void Dispose()
        {
            Interlocked.Exchange(ref _isShutDownRequested, 1);

            if (_observable != null)
            {
                _observable = null;
            }
        }

        internal void Subscribe(IObserver<string> subscriber)
        {
            if (!_observers.Contains(subscriber))
            {
                _observers.Add(subscriber);
                _observable.Subscribe(subscriber);
            }
        }

        /*internal event EventHandler<SocketMessageReceivedEventArgs> OnMessageReceived;

        public void Dispose()
        {
            this.Close();
        }

        private void Close()
        {
            lock (this)
            {
                if (_token != null
                    && !_token.IsCancellationRequested)
                {
                    _tokenSource.Cancel();
                }

                if (_socket != null
                    && (_socket.State == WebSocketState.Connecting
                        || _socket.State == WebSocketState.Open))
                {
                    _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None).Wait();
                    _socket.Dispose();
                    _socket = null;
                }

                if (_observer != null)
                {
                    _observer.Wait();
                    _observer.Dispose();
                    _observer = null;
                }
            }
        }

        private void Monitor()
        {
            lock (this)
            {
                if (_dispatch == null)
                {
                    _dispatch = new MessageDispatch();
                }
                _socket = new ClientWebSocket();

                _socket.ConnectAsync(_socketUri, CancellationToken.None).Wait();

                if (_observer == null)
                {
                    this._observer = Task.Run((() =>
                    {
                        Task.WhenAll(Receive(_socket));
                    }));
                }
            }
        }

        private void RaiseMessageReceived(string message)
        {
            if (this.OnMessageReceived != null)
            {
                OnMessageReceived(this, new SocketMessageReceivedEventArgs(message));
            }
        }

        private async Task Receive(ClientWebSocket socket)
        {
            string fragment = string.Empty;

            while (socket.State == WebSocketState.Open)
            {
                byte[] buffer = new byte[_chunkSize];

                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    fragment += _encoding.GetString(buffer);

                    if (result.EndOfMessage)
                    {
                        _dispatch.EnqueueMessage(fragment);

                        fragment = string.Empty;
                    }
                }
            }
        }*/

        private async Task Send(ClientWebSocket socket, object message)
        {
            /*
            var message = new SubscribeToTradingPair();

            message.type = "subscribe";

            message.product_id = "BTC-USD";*/

            var json = JsonConvert.SerializeObject(message);

            var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(json));

            await socket.SendAsync(
                buffer,
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);
        }

        internal IObservable<string> MessageBuffer(object subscription)
        {
            var ob = Observable.Create<string>(sub =>
            {
                return NewThreadScheduler.Default.ScheduleLongRunning(cancel =>
                {
                    using (var _sock = new ClientWebSocket())
                    {
                        Debug.Log(string.Format("Connecting Socket {0}...", _socketUri.AbsoluteUri));

                        _sock.ConnectAsync(_socketUri, CancellationToken.None).Wait();

                        Debug.Log(string.Format("Connection Result {0}...", _sock.State));

                        var fragment = string.Empty;

                        Debug.Log("Subscribing...");

                        Send(_sock, subscription).Wait();

                        Debug.Log("Begin Receive");

                        while (_sock.State == WebSocketState.Open && _isShutDownRequested == 0)
                        {
                            var buffer = new byte[_chunkSize];

                            var result =
                                _sock.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None).Result;

                            if (result.MessageType == WebSocketMessageType.Close)
                            {
                                _sock.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty,
                                    CancellationToken.None);
                            }
                            else
                            {
                                fragment += _encoding.GetString(buffer);

                                if (result.EndOfMessage)
                                {
                                    sub.OnNext(fragment);

                                    fragment = string.Empty;
                                }
                            }
                        }
                    }

                    Debug.Log(string.Format("Close {0}", _socketUri.AbsoluteUri));
                });
            });

            return ob;
        }
    }
}