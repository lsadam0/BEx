using BEx.ExchangeEngine;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;

namespace BEx.UnitTests.MockTests.MockObjects
{
    public class MockExchangeConfiguration : IExchangeConfiguration
    {
        private long _nonce = DateTime.UtcNow.Ticks;

        internal MockExchangeConfiguration()
        {
            Initialize(null);
        }

        public TradingPair DefaultPair
        {
            get;
            private set;
        }

        public ImmutableHashSet<TradingPair> SupportedPairs
        {
            get;
            private set;
        }

        public ImmutableHashSet<Currency> SupportedCurrencies
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

        /// <summary>
        /// Consecutively increasing action counter
        /// </summary>
        /// <value>0</value>
        public long Nonce
        {
            get
            {
                return Interlocked.Increment(ref _nonce);
            }
        }

        private void Initialize(Uri baseUri)
        {
            DefaultPair = new TradingPair(Currency.BTC, Currency.USD);

            ExchangeSourceType = ExchangeType.Mock;

            SupportedPairs = new HashSet<TradingPair>()
            {
                DefaultPair,
                new TradingPair(Currency.LTC, Currency.USD),
                new TradingPair(Currency.LTC, Currency.BTC)
            }.ToImmutableHashSet<TradingPair>();

            SupportedCurrencies = new HashSet<Currency>()
            {
                DefaultPair.BaseCurrency,
                DefaultPair.CounterCurrency,
                Currency.LTC
            }.ToImmutableHashSet<Currency>();
            
            BaseUri = baseUri ?? new Uri("http://localhost");
        }
    }
}