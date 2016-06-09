using System;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Gdax;
using BEx.ExchangeEngine.Gdax.API;
using BEx.ExchangeEngine.Gdax.WebSocket.JSON;
using BEx.ExchangeEngine.Gdax.WebSocket;

namespace BEx
{
    public sealed class Gdax : Exchange
    {
        public Gdax()
            : base(Configuration.Singleton,
                CommandFactory.Singleton)
        {
        }

        public Gdax(string apiKey, string secret, string passphrase)
            : base(Configuration.Singleton,
                CommandFactory.Singleton,
                new Authenticator(apiKey, secret, passphrase))
        {
        }

        protected override void Subscribe()
        {
            var message = new SubscribeToTradingPair()
            {
                type = "subscribe",
                product_id = "BTC-USD"
            };

            this._socketObservable = new ExchangeEngine.SocketObservable(
                Configuration.Singleton,
                message);

            this._socketObserver = new ExchangeEngine.SocketObserver(new GdaxParser());

            this._socketObservable.Subscribe(_socketObserver);

        }
    }
}