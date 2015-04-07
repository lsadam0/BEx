using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BEx.ExchangeEngine;

namespace BEx.UnitTests.MockTests.MockObjects
{
    public class MockExchangeConfiguration : IExchangeConfiguration
    {
        private long _nonce = DateTime.UtcNow.Ticks;



        internal MockExchangeConfiguration()
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
            DefaultPair = new CurrencyTradingPair(Currency.BTC, Currency.USD);

            ExchangeSourceType = ExchangeType.Mock;

            SupportedPairs = new HashSet<CurrencyTradingPair>()
            {
                DefaultPair,
                new CurrencyTradingPair(Currency.LTC, Currency.USD),
                new CurrencyTradingPair(Currency.LTC, Currency.BTC)
            };

            SupportedCurrencies = new HashSet<Currency>()
            {
                DefaultPair.BaseCurrency,
                DefaultPair.CounterCurrency,
                Currency.LTC
            };


            BaseUri = baseUri ?? new Uri("http://localhost");
        }
    }
}
