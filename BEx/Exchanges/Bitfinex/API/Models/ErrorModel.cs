using BEx.ExchangeEngine;
using Newtonsoft.Json;

using BEx.ExchangeEngine.API;

namespace BEx.Exchanges.Bitfinex.API.Models
{
    internal class ErrorModel : IExchangeResponseIntermediate<BExError>
    {
        [JsonProperty("message")]
        public string message { get; set; }

        public BExError Convert(TradingPair pair)
        {
            return new BExError(ExchangeType.Bitfinex)
            {
                Message = message
            };
        }
    }
}