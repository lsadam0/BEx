using System;
using System.Collections.Generic;

namespace BEx.ExchangeEngine
{
    public interface IExchangeConfiguration
    {
        string ApiKey
        {
            get;
        }

        string ClientId
        {
            get;
        }

        CurrencyTradingPair DefaultPair
        {
            get;
        }

        string SecretKey
        {
            get;
        }

        IList<CurrencyTradingPair> SupportedPairs
        {
            get;
        }

        HashSet<Currency> SupportedCurrencies
        {
            get;
        }

        long Nonce
        {
            get;
        }

        Uri BaseUri
        {
            get;
        }
    }
}