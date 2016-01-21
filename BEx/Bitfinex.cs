﻿// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine.Bitfinex;

namespace BEx
{
    public sealed class Bitfinex : Exchange
    {
        public Bitfinex()
            : base(
                BitfinexConfiguration.Singleton,
                BitfinexCommandFactory.Singleton)
        {
        }

        public Bitfinex(string apiKey, string secret)
            : base(
                BitfinexConfiguration.Singleton,
                BitfinexCommandFactory.Singleton,
                new BitfinexAuthenticator(secret, apiKey))
        {
        }
    }
}