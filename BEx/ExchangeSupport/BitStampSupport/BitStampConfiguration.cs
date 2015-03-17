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
            set;
        }

        public string ClientId
        {
            get;
            set;
        }

        public CurrencyTradingPair DefaultPair
        {
            get;
            set;
        }

        public string SecretKey
        {
            get;
            set;
        }

        public IList<CurrencyTradingPair> SupportedPairs
        {
            get;
            set;
        }

        public HashSet<Currency> SupportedCurrencies
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
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

        private void Initialize(string url = null)
        {
            DefaultPair = new CurrencyTradingPair(Currency.BTC, Currency.USD);

            SupportedPairs = new List<CurrencyTradingPair>() { DefaultPair };

            SupportedCurrencies = new HashSet<Currency>();

            SupportedCurrencies.Add(DefaultPair.BaseCurrency);
            SupportedCurrencies.Add(DefaultPair.CounterCurrency);

            if (string.IsNullOrWhiteSpace(url))
                Url = "https://www.bitstamp.net/api";
            else
                Url = url.TrimEnd('/', '\\');
        }

        internal BitStampConfiguration()
        {
            Initialize();
        }

        public BitStampConfiguration(string apiKey, string clientId, string secretKey, string url = null)
        {
            ApiKey = apiKey;
            ClientId = clientId;
            SecretKey = secretKey;

            Initialize(url);
        }
    }
}