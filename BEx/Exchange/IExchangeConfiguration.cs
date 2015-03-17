using System.Collections.Generic;

namespace BEx.Request
{
    public interface IExchangeConfiguration
    {
        string ApiKey
        {
            get;
            set;
        }

        string ClientId
        {
            get;
            set;
        }

        CurrencyTradingPair DefaultPair
        {
            get;
            set;
        }

        string SecretKey
        {
            get;
            set;
        }

        IList<CurrencyTradingPair> SupportedPairs
        {
            get;
            set;
        }

        HashSet<Currency> SupportedCurrencies
        {
            get;
            set;
        }

        long Nonce
        {
            get;
        }

        string Url
        {
            get;
            set;
        }
    }
}