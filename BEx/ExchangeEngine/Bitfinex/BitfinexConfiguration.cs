// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx.ExchangeEngine.Bitfinex.JSON;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

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

        public Uri BaseUri
        {
            get;
            private set;
        }

        public string ClientId
        {
            get;
            private set;
        }

        public TradingPair DefaultPair
        {
            get;
            private set;
        }

        public Type ErrorJsonType
        {
            get;
            private set;
        }

        public ExchangeType ExchangeSourceType
        {
            get;
            private set;
        }

        public string SecretKey
        {
            get;
            set;
        }

        public ImmutableHashSet<Currency> SupportedCurrencies
        {
            get;
            private set;
        }

        public ImmutableHashSet<TradingPair> SupportedPairs
        {
            get;
            private set;
        }

        private void Initialize(Uri baseUri)
        {
            ErrorJsonType = typeof(ErrorIntermediate);
            DefaultPair = new TradingPair(Currency.BTC, Currency.USD);
            ExchangeSourceType = ExchangeType.Bitfinex;
            ExchangeSourceType = ExchangeType.Bitfinex;

            SupportedPairs = ImmutableHashSet.Create<TradingPair>(
                DefaultPair,
                new TradingPair(Currency.LTC, Currency.USD),
                new TradingPair(Currency.LTC, Currency.BTC)

                );

            var supportedCurrencies = new HashSet<Currency>();

            foreach (var pair in SupportedPairs)
            {
                if (!supportedCurrencies.Contains(pair.BaseCurrency))
                {
                    supportedCurrencies.Add(pair.BaseCurrency);
                }

                if (!supportedCurrencies.Contains(pair.CounterCurrency))
                {
                    supportedCurrencies.Add(pair.CounterCurrency);
                }
            }

            this.SupportedCurrencies = supportedCurrencies.ToImmutableHashSet<Currency>();

            BaseUri = baseUri ?? new Uri("https://api.bitfinex.com");
        }
    }
}