using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.BitfinexSupport
{

    internal class BitfinexErrorJSON : IExchangeResponse
    {
        [JsonProperty("message")]
        public string message { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {
            return new ApiError(sourceExchange.ExchangeSourceType)
            {
                Message = message
            };
        }
    }
}
