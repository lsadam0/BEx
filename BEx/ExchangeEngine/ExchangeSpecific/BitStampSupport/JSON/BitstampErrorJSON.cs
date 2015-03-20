// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace BEx.ExchangeEngine.BitStampSupport
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