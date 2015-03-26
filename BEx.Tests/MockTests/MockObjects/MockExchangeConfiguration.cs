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
        private long _nonce = DateTime.Now.Ticks;



        internal MockExchangeConfiguration()
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

        public IList<CurrencyTradingPair> SupportedPairs
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

            ApiKey = "mocked";
            ClientId = "mocked";
            SecretKey = "mocked";

            ExchangeSourceType = ExchangeType.Mock;

            SupportedPairs = new List<CurrencyTradingPair>()
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
