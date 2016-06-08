using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Gdax;
using BEx.ExchangeEngine.Gdax.JSON.WebSocket;
using NUnit.Framework;

namespace BEx.Tests
{
    [TestFixture]
    public class Scratch
    {
        [Test]
        public void Socket()
        {
            var conf = Configuration.Singleton;

            var message = new SubscribeToTradingPair();

            message.type = "subscribe";

            message.product_id = "BTC-USD";

            var obs = new ExchangeSocketObserver(conf, message);

            var dispatch = new MessageDispatch(new GdaxParser());

            obs.Subscribe(dispatch);
            // obs.Begin();
            //    obs.Subscribe(new TradingPair(Currency.BTC, Currency.USD));

            var run = true;

            while (run)
            {
                {
                }
            }
        }
    }
}