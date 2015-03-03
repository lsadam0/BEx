using Newtonsoft.Json;
using System;

namespace BEx.BitFinexSupport
{
    public class BitFinexDepositAddressJSON : ExchangeResponse<DepositAddress>
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        public override DepositAddress ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            return new DepositAddress(Address, DateTime.Now, baseCurrency);
        }
    }
}