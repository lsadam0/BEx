// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Bitfinex;
using BEx.Exchanges.Bitfinex.API;
using BEx.Exchanges.Bitfinex.WebSocket;
using BEx.Exchanges.Bitfinex.WebSocket.Models;

namespace BEx
{
    public sealed class Bitfinex : Exchange
    {
        public Bitfinex()
            : base(
                Configuration.Singleton,
                CommandFactory.Singleton)
        {
        }

        public Bitfinex(string apiKey, string secret)
            : base(
                Configuration.Singleton,
                CommandFactory.Singleton,
                new Authenticator(secret, apiKey))
        {
        }

        protected override void Subscribe()
        {
            var message = new SubscribeToChannelModel
            {
                _event = "subscribe",
                channel = "ticker",
                pair = "BTCUSD"
            };

            _socketObservable = new SocketObservable(
                Configuration.Singleton,
                message);

            _socketObserver = new SocketObserver(new Parser());

            _socketObservable.Subscribe(_socketObserver);
        }
    }
}