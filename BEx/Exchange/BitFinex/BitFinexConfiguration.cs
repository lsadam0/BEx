using BEx.Request;
using System.Collections.Generic;

namespace BEx.BitFinexSupport
{
    public class BitFinexConfiguration : IExchangeConfiguration
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

        private void Initialize(string url = null)
        {
            DefaultPair = new CurrencyTradingPair(Currency.BTC, Currency.USD);

            SupportedPairs = new List<CurrencyTradingPair>() { DefaultPair };

            SupportedPairs.Add(new CurrencyTradingPair(Currency.LTC, Currency.USD));
            SupportedPairs.Add(new CurrencyTradingPair(Currency.LTC, Currency.BTC));
            SupportedPairs.Add(new CurrencyTradingPair(Currency.DRK, Currency.USD));
            SupportedPairs.Add(new CurrencyTradingPair(Currency.DRK, Currency.BTC));

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

        internal BitFinexConfiguration()
        {
            Initialize();
        }

        public BitFinexConfiguration(string apiKey, string secretKey, string url = null)
        {
            ApiKey = apiKey;
            SecretKey = secretKey;

            Initialize(url);
        }
    }
}