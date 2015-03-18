using Newtonsoft.Json;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitStampErrorJSON : IExchangeResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        public APIResult ConvertToStandard(CurrencyTradingPair pair)
        {
            APIError error = new APIError(ExchangeType.BitStamp);

            error.Message = Error;

            return error;
        }
    }
}