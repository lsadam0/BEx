// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Threading;

namespace BEx.ExchangeEngine.BitfinexSupport
{
    public class BitfinexConfiguration : IExchangeConfiguration
    {
        private long _nonce = DateTime.Now.Ticks;

        public BitfinexConfiguration(string apiKey, string secretKey)
        {
            ApiKey = apiKey;
            SecretKey = secretKey;

            Initialize(null);
        }

        public BitfinexConfiguration(string apiKey, string secretKey, Uri baseUri)
        {
            ApiKey = apiKey;
            SecretKey = secretKey;

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
            private set;
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

            SupportedPairs = new List<CurrencyTradingPair>()
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