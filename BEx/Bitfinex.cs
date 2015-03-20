using BEx.ExchangeSupport.BitfinexSupport;

namespace BEx
{
    public sealed class Bitfinex : Exchange
    {
        public Bitfinex()
            : base(new BitfinexConfiguration(), new BitfinexCommandFactory(), ExchangeType.Bitfinex)
        {
        }

        public Bitfinex(string apiKey, string secret)
            : base(new BitfinexConfiguration(apiKey, secret), new BitfinexCommandFactory(), ExchangeType.Bitfinex)
        {
            Authenticator = new BitfinexAuthenticator(Configuration);
        }
    }
}