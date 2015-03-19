using System;
using System.Collections.Generic;
using System.Threading;

namespace BEx.ExchangeSupport.BitStampSupport
{
    public class BitStampConfiguration : IExchangeConfiguration
    {
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

        private long _nonce = DateTime.Now.Ticks;

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

            SupportedPairs = new List<CurrencyTradingPair>() { DefaultPair };

            SupportedCurrencies = new HashSet<Currency>();

            SupportedCurrencies.Add(DefaultPair.BaseCurrency);
            SupportedCurrencies.Add(DefaultPair.CounterCurrency);

            if (baseUri != null)
                BaseUri = new Uri("https://www.bitstamp.net/api");
            else
                BaseUri = baseUri;
        }

        internal BitStampConfiguration()
        {
            Initialize(null);
        }

        public BitStampConfiguration(string apiKey, string clientId, string secretKey)
        {
            ApiKey = apiKey;
            ClientId = clientId;
            SecretKey = secretKey;

            Initialize(null);
        }

        public BitStampConfiguration(string apiKey, string clientId, string secretKey, Uri baseUri)
        {
            ApiKey = apiKey;
            ClientId = clientId;
            SecretKey = secretKey;

            Initialize(baseUri);
        }
    }
}