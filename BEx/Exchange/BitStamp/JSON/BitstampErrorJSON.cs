using Newtonsoft.Json;

namespace BEx.BitStampSupport
{
    public class BitStampErrorJSON
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        public APIError ConvertToStandard(CurrencyTradingPair pair)
        {
            APIError error = new APIError(ExchangeType.BitStamp);

            error.Message = Error;

            return error;
        }
    }
}