using Newtonsoft.Json;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitStampErrorJSON : IExchangeResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            ApiError error = new ApiError(ExchangeType.BitStamp);

            error.Message = Error;

            return error;
        }
    }
}