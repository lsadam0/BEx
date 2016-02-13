// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BEx.ExchangeEngine.Bitfinex.JSON.ResponseIntermediates;

namespace BEx.ExchangeEngine.Bitfinex
{
    public class Configuration : IExchangeConfiguration
    {
        private static readonly Configuration Instance = new Configuration();

        private Configuration()
        {
            Initialize(null);
        }

        public static IExchangeConfiguration Singleton => Instance;

        public Uri BaseUri { get; private set; }

        public Uri WebSocketUri { get; private set; }

        public TradingPair DefaultPair { get; private set; }
        public Type ErrorJsonType { get; private set; }
        public ExchangeType ExchangeSourceType { get; private set; }
        public ImmutableHashSet<Currency> SupportedCurrencies { get; private set; }

        public ImmutableHashSet<TradingPair> SupportedPairs { get; private set; }

        private void Initialize(Uri baseUri)
        {
            ErrorJsonType = typeof (ErrorIntermediate);
            DefaultPair = new TradingPair(Currency.BTC, Currency.USD);
            ExchangeSourceType = ExchangeType.Bitfinex;
            ExchangeSourceType = ExchangeType.Bitfinex;

            SupportedPairs = ImmutableHashSet.Create(
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

            SupportedCurrencies = supportedCurrencies.ToImmutableHashSet();

            BaseUri = baseUri ?? new Uri("https://api.bitfinex.com");
            WebSocketUri = new Uri(@"wss://api2.bitfinex.com:3000/ws");
        }
    }
}