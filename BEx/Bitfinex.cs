// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine.BitfinexSupport;

namespace BEx
{
    public sealed class Bitfinex : Exchange
    {
        public Bitfinex()
            : base(new BitfinexConfiguration(), new BitfinexCommandFactory())
        {
        }

        public Bitfinex(string apiKey, string secret)
            : base(new BitfinexConfiguration(apiKey, secret), new BitfinexCommandFactory())
        {
            Authenticator = new BitfinexAuthenticator(Configuration);
        }
    }
}