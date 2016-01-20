// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BEx.ExchangeEngine.BitStamp.JSON;

namespace BEx.ExchangeEngine.BitStamp
{
    public class BitStampConfiguration : IExchangeConfiguration
    {
        private static BitStampConfiguration instance = new BitStampConfiguration();

        public static IExchangeConfiguration Singleton
        {
            get
            {
                return instance;
            }
        }

        /*
        public BitStampConfiguration(Uri baseUri)
        {
            Initialize(baseUri);
        }
        */

        internal BitStampConfiguration()
        {
            Initialize(null);
        }

        public Uri BaseUri
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
            ExchangeSourceType = ExchangeType.BitStamp;

            SupportedPairs = new HashSet<TradingPair>()
            {
                DefaultPair
            }.ToImmutableHashSet<TradingPair>();

            SupportedCurrencies = new HashSet<Currency>()
            {
                DefaultPair.BaseCurrency,
                DefaultPair.CounterCurrency
            }.ToImmutableHashSet<Currency>();

            BaseUri = baseUri ?? new Uri("https://www.bitstamp.net/api");
        }
    }
}