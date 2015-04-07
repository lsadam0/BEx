// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Threading;
using BEx.ExchangeEngine.Bitfinex.JSON;

namespace BEx.ExchangeEngine.Bitfinex
{
    public class BitfinexConfiguration : IExchangeConfiguration
    {

        public BitfinexConfiguration(Uri baseUri)
        {
            Initialize(baseUri);
        }

        internal BitfinexConfiguration()
        {
            Initialize(null);
        }

        public string ApiKey
        {
            get;
            private set;
        }

        public string ClientId
        {
            get;
            private set;
        }

        public CurrencyTradingPair DefaultPair
        {
            get;
            private set;
        }

        public string SecretKey
        {
            get;
            set;
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
            ExchangeSourceType = ExchangeType.Bitfinex;
            ExchangeSourceType = ExchangeType.Bitfinex;

            SupportedPairs = new HashSet<CurrencyTradingPair>()
            {
                DefaultPair,
                new CurrencyTradingPair(Currency.LTC, Currency.USD),
                new CurrencyTradingPair(Currency.LTC, Currency.BTC),
                new CurrencyTradingPair(Currency.DRK, Currency.USD),
                new CurrencyTradingPair(Currency.DRK, Currency.BTC)
            };

            SupportedCurrencies = new HashSet<Currency>();

            foreach (var pair in SupportedPairs)
            {
                if (!SupportedCurrencies.Contains(pair.BaseCurrency))
                    SupportedCurrencies.Add(pair.BaseCurrency);

                if (!SupportedCurrencies.Contains(pair.CounterCurrency))
                    SupportedCurrencies.Add(pair.CounterCurrency);

            }

            BaseUri = baseUri ?? new Uri("https://api.bitfinex.com");
        }
    }
}