using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.WebSockets;
using Newtonsoft.Json;

using BEx.ExchangeEngine.Coinbase.WebSocket;
namespace BEx.ExchangeEngine
{
    public class ExchangeSocketObserver : IDisposable
    {
        private int _chunkSize = 1024;
        private Uri _socketUri;

        private UTF8Encoding _encoding = new UTF8Encoding();
        private ClientWebSocket _socket;
        public ExchangeSocketObserver(IExchangeConfiguration configuration)
        {
            this._socketUri = configuration.WebSocketUri;
        }


        public void Dispose()
        {
            if (_socket != null
                && (_socket.State == WebSocketState.Connecting
                || _socket.State == WebSocketState.Open))
            {
                _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None).Wait();
                _socket.Dispose();
                _socket = null;

            }
        }

        public async void Begin()
        {
            _socket = new ClientWebSocket();

            _socket.ConnectAsync(_socketUri, CancellationToken.None).Wait();

            Task.WhenAll(Receive(_socket));

        }
        private void Log(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {

                Trace.WriteLine(message);
                Trace.WriteLine(Environment.NewLine);
            }


        }

        public void Subscribe(TradingPair pair)
        {
            // byte[] buffer = new byte[_chunkSize];

            var message = new SubscribeToTradingPair();
            message.type = "subscribe";
            message.product_id = "BTC-USD";

            var serializer = new JsonSerializer();

            var json = JsonConvert.SerializeObject(message);
            ArraySegment<byte> buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(json));

            _socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
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
                        Log(fragment);
                        fragment = string.Empty;
                    }
                }

            }
        }

        private async Task Send(ClientWebSocket socket)
        {

        }
    }
}
