using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Debug = BEx.ExchangeEngine.Utilities.Debug;

namespace BEx.ExchangeEngine
{
    public class SocketObservable : IDisposable
    {
        // Why this size?
        private readonly int _chunkSize = 1024;

        private readonly UTF8Encoding _encoding = new UTF8Encoding();
        private readonly HashSet<IObserver<string>> _observers = new HashSet<IObserver<string>>();
        private readonly Uri _socketUri;
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private IObservable<string> _observable;

        public SocketObservable(IExchangeConfiguration configuration, object subscribe)
        {
            _socketUri = configuration.WebSocketUri;

            _observable = MessageBuffer(subscribe);
        }

        public void Dispose()
        {
            _tokenSource.Cancel();

            _observable = null;
        }

        internal void Subscribe(IObserver<string> subscriber)
        {
            if (!_observers.Contains(subscriber))
            {
                _observers.Add(subscriber);
                _observable.Subscribe(subscriber);
            }
        }

        private void Connect(ClientWebSocket socket, object subscription)
        {
            try
            {
                Debug.Log($"Connecting Socket {_socketUri.AbsoluteUri}...");

                socket.ConnectAsync(_socketUri, _tokenSource.Token).Wait();

                Debug.Log($"Connection Result {socket.State}...");

                Send(socket, subscription).Wait();

                Debug.Log("Subscribed.");
            }
            catch (Exception e)
            {
                Trace.TraceError($"Socket connection {_socketUri} failed: {e}");
            }
            finally
            {
                if (socket.State != WebSocketState.Open)
                {
                    Trace.TraceError($"Socket connection failed.  Retry in 10 seconds.");
                    Task.Delay(10000);
                }
            }
        }

        private IObservable<string> MessageBuffer(object subscription)
        {
            var ob = Observable.Create<string>(sub =>
            {
                return NewThreadScheduler.Default.ScheduleLongRunning(cancel =>
                {
                    using (var clientWebSocket = new ClientWebSocket())
                    {
                        try
                        {
                            var fragment = string.Empty;

                            while (!_tokenSource.IsCancellationRequested)
                            {
                                if (clientWebSocket.State != WebSocketState.Open)
                                {
                                    Connect(clientWebSocket, subscription);
                                    fragment = string.Empty;
                                }
                                else
                                {
                                    var buffer = new byte[_chunkSize];

                                    var result =
                                        clientWebSocket.ReceiveAsync(
                                            new ArraySegment<byte>(buffer),
                                            _tokenSource.Token
                                            )
                                            .Result;

                                    if (result.MessageType == WebSocketMessageType.Close)
                                    {
                                        Debug.Log($"Close message received. Closing {_socketUri}");

                                        clientWebSocket.CloseAsync(
                                            WebSocketCloseStatus.NormalClosure,
                                            string.Empty,
                                            _tokenSource.Token);
                                    }
                                    else
                                    {
                                        fragment += _encoding.GetString(buffer);

                                        if (!result.EndOfMessage) continue;

                                        // Message complete, broadcast.
                                        sub.OnNext(fragment);

                                        fragment = string.Empty;
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        finally
                        {
                            if (clientWebSocket.State == WebSocketState.Open
                                || clientWebSocket.State == WebSocketState.Connecting)
                            {
                                clientWebSocket.CloseAsync(
                                    WebSocketCloseStatus.NormalClosure,
                                    string.Empty,
                                    _tokenSource.Token);
                            }

                            SignalEndToAllObeservers();
                        }
                    }

                    Debug.Log($"Close {_socketUri.AbsoluteUri}");
                });
            });

            return ob;
        }

        private async Task Send(ClientWebSocket socket, object message)
        {
            var json = JsonConvert.SerializeObject(message);

            var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(json));

            await socket.SendAsync(
                buffer,
                WebSocketMessageType.Text,
                true,
                _tokenSource.Token);
        }

        private void SignalEndToAllObeservers()
        {
            _observers
                .AsParallel()
                .ForAll(o => o.OnCompleted());
        }
    }
}