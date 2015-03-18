using System;
using System.Collections.Generic;
using System.Threading;

namespace BEx.ExchangeSupport.BitfinexSupport
{
    public class BitfinexConfiguration : IExchangeConfiguration
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
            DefaultPair = new CurrencyTradingPair(Currency.Btc, Currency.Usd);

            SupportedPairs = new List<CurrencyTradingPair>() { DefaultPair };

            SupportedPairs.Add(new CurrencyTradingPair(Currency.Ltc, Currency.Usd));
            SupportedPairs.Add(new CurrencyTradingPair(Currency.Ltc, Currency.Btc));
            SupportedPairs.Add(new CurrencyTradingPair(Currency.Drk, Currency.Usd));
            SupportedPairs.Add(new CurrencyTradingPair(Currency.Drk, Currency.Btc));

            SupportedCurrencies = new HashSet<Currency>();

            foreach (CurrencyTradingPair pair in SupportedPairs)
            {
                if (!SupportedCurrencies.Contains(pair.BaseCurrency))
                    SupportedCurrencies.Add(pair.BaseCurrency);

                if (!SupportedCurrencies.Contains(pair.CounterCurrency))
                    SupportedCurrencies.Add(pair.CounterCurrency);
            }

            if (string.IsNullOrWhiteSpace(url))
                Url = "https://api.bitfinex.com";
            else
                Url = url.TrimEnd('/', '\\');
        }

        internal BitfinexConfiguration()
        {
            Initialize();
        }

        public BitfinexConfiguration(string apiKey, string secretKey, string url = null)
        {
            ApiKey = apiKey;
            SecretKey = secretKey;

            Initialize(url);
        }
    }
}