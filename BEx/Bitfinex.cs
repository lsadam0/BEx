using BEx.ExchangeSupport.BitfinexSupport;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

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
            Authenticator = new BitfinexAuthenticator(base.Configuration);
        }
    }
}