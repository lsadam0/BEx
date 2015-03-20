using System;
using Newtonsoft.Json;


namespace BEx.ExchangeSupport.BitfinexSupport
{
    internal class BitFinexDepositAddressJSON : IExchangeResponse
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            return new DepositAddress(Address, DateTime.Now, pair.BaseCurrency, ExchangeType.Bitfinex);
        }
    }
}