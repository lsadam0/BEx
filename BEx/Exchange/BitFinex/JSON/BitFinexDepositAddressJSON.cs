using Newtonsoft.Json;

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
            DepositAddress res = new DepositAddress();

            res.Address = Address;
            res.DepositCurrency = baseCurrency;

            return res;
        }

        /*
        public DepositAddress ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            DepositAddress res = new DepositAddress();

            res.Address = Address;
            res.DepositCurrency = baseCurrency;

            return res;
        }*/
    }
}