using Newtonsoft.Json;

namespace BEx.ExchangeEngine.Bitfinex.JSON
{
    internal class ErrorIntermediate : IExchangeResponse
    {
        [JsonProperty("message")]
        public string message { get; set; }

        public BExResult ConvertToStandard(CurrencyTradingPair pair, Exchange sourceExchange)
        {
            return new BExError(sourceExchange.ExchangeSourceType)
            {
                Message = message
            };
        }
    }
}