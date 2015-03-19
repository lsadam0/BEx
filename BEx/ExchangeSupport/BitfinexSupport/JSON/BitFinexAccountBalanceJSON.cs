using Newtonsoft.Json;
using System;
using System.Globalization;

namespace BEx.ExchangeSupport.BitfinexSupport
{
    internal class BitFinexAccountBalanceJSON : IExchangeResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("available")]
        public string Available { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            Balance res = null;

            if (Type == "exchange")
            {
                res = new Balance(DateTime.Now, ExchangeType.Bitfinex);
                Currency bCurrency;

                if (Enum.TryParse<Currency>(Currency.ToUpper(CultureInfo.InvariantCulture), out bCurrency))
                {
                    res.BalanceCurrency = bCurrency;
                    res.AvailableToTrade = Conversion.ToDecimalInvariant(Available);
                    res.TotalBalance = Conversion.ToDecimalInvariant(Amount);
                }
            }

            return res;
        }
    }
}