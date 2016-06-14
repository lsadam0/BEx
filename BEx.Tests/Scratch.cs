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

            var message = new SubscribeToTradingPairModel();

            message.type = "subscribe";

            message.product_id = "BTC-USD";

            var obs = new SocketObservable(conf, message);

            var dispatch = new SocketObserver(new GdaxParser());

            obs.Subscribe(dispatch);
            */

            //var gdax = new Gdax();
            var bfx = new Bitfinex();
            var run = true;

            while (run)
            {
                {
                }
            }
        }
    }
}