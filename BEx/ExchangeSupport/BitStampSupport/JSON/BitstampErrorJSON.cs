using Newtonsoft.Json;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitStampErrorJSON : IExchangeResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            return new ApiError(ExchangeType.BitStamp)
            {
                Message = Error
            };
        }
    }
}