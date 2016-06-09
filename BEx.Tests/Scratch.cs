using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Gdax;
using BEx.ExchangeEngine.Gdax.WebSocket.JSON;
using NUnit.Framework;

namespace BEx.Tests
{
    [TestFixture]
    public class Scratch
    {
        [Test]
        public void Socket()
        {
            /*
            var conf = Configuration.Singleton;

            var message = new SubscribeToTradingPair();

            message.type = "subscribe";

            message.product_id = "BTC-USD";

            var obs = new SocketObservable(conf, message);

            var dispatch = new SocketObserver(new GdaxParser());

            obs.Subscribe(dispatch);
            */

            var gdax = new Gdax();

            var run = true;

            while (run)
            {
                {
                }
            }
        }
    }
}