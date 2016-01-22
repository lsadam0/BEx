// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BEx.ExchangeEngine.BitStamp.JSON.ResponseIntermediates;

namespace BEx.ExchangeEngine.BitStamp
{
    public class BitStampConfiguration : IExchangeConfiguration
    {
        private static readonly BitStampConfiguration Instance = new BitStampConfiguration();

        internal BitStampConfiguration()
        {
            Initialize(null);
        }

        public static IExchangeConfiguration Singleton => Instance;

        public Uri BaseUri { get; private set; }

        public TradingPair DefaultPair { get; private set; }

        public Type ErrorJsonType { get; private set; }

        public ExchangeType ExchangeSourceType { get; private set; }

        public ImmutableHashSet<Currency> SupportedCurrencies { get; private set; }

        public ImmutableHashSet<TradingPair> SupportedPairs { get; private set; }

        private void Initialize(Uri baseUri)
        {
            ErrorJsonType = typeof(ErrorIntermediate);
            DefaultPair = new TradingPair(Currency.BTC, Currency.USD);
            ExchangeSourceType = ExchangeType.BitStamp;

            SupportedPairs = new HashSet<TradingPair>
            {
                DefaultPair
            }.ToImmutableHashSet();

            SupportedCurrencies = new HashSet<Currency>
            {
                DefaultPair.BaseCurrency,
                DefaultPair.CounterCurrency
            }.ToImmutableHashSet();

            BaseUri = baseUri ?? new Uri("https://www.bitstamp.net/api");
        }
    }
}