// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine.Bitfinex;

namespace BEx
{
    public sealed class Bitfinex : Exchange
    {
        public Bitfinex()
            : base(
                    new BitfinexConfiguration(),
                    new BitfinexCommandFactory(),
                    BitfinexErrorInterpreter.GetInterpreter())
        {
        }

        public Bitfinex(string apiKey, string secret)
            : base(
                    new BitfinexConfiguration(),
                    new BitfinexCommandFactory(),
                    BitfinexErrorInterpreter.GetInterpreter(),
                    new BitfinexAuthenticator(secret, apiKey))
        {
        }
    }
}