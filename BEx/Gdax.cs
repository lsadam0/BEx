using BEx.ExchangeEngine;
using BEx.Exchanges.Gdax;
using BEx.Exchanges.Gdax.API;
using BEx.Exchanges.Gdax.WebSocket;
using BEx.Exchanges.Gdax.WebSocket.Models;

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
            var message = new SubscribeToTradingPairModel
            {
                type = "subscribe",
                product_id = "BTC-USD"
            };

            _socketObservable = new SocketObservable(
                Configuration.Singleton,
                message);

            _socketObserver = new SocketObserver(new GdaxParser());

            _socketObservable.Subscribe(_socketObserver);
        }
    }
}