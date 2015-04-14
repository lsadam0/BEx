// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Threading;
using BEx.ExchangeEngine.BitStamp.JSON;

namespace BEx.ExchangeEngine.BitStamp
{
    public class BitStampConfiguration : IExchangeConfiguration
    {
        public BitStampConfiguration(Uri baseUri)
        {
            Initialize(baseUri);
        }

        internal BitStampConfiguration()
        {
            Initialize(null);
        }

        public CurrencyTradingPair DefaultPair
        {
            get;
            private set;
        }

        public HashSet<CurrencyTradingPair> SupportedPairs
        {
            get;
            private set;
        }

        public HashSet<Currency> SupportedCurrencies
        {
            get;
            private set;
        }

        public Uri BaseUri
        {
            get;
            private set;
        }

        public ExchangeType ExchangeSourceType
        {
            get;
            private set;
        }

        public Type ErrorJsonType
        {
            get;
            private set;
        }

        private void Initialize(Uri baseUri)
        {
            ErrorJsonType = typeof(ErrorIntermediate);
            DefaultPair = new CurrencyTradingPair(Currency.BTC, Currency.USD);
            ExchangeSourceType = ExchangeType.BitStamp;
            SupportedPairs = new HashSet<CurrencyTradingPair>() { DefaultPair };

            SupportedCurrencies = new HashSet<Currency>()
            {
                DefaultPair.BaseCurrency,
                DefaultPair.CounterCurrency
            };

            BaseUri = baseUri ?? new Uri("https://www.bitstamp.net/api");
        }
    }
}