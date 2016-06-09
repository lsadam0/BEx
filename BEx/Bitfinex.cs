// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using BEx.ExchangeEngine.Bitfinex;

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
            
        }
    }
}