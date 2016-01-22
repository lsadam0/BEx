using Newtonsoft.Json;

namespace BEx.ExchangeEngine.Bitfinex.JSON.ResponseIntermediates
{
    internal class ErrorIntermediate : IExchangeResponse<BExError>
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