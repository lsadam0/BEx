using Newtonsoft.Json;

namespace BEx.ExchangeEngine.Bitfinex.JSON
{
    internal class ErrorIntermediate : IExchangeResponse<BExError>
    {
        [JsonProperty("message")]
        public string message { get; set; }


        public BExError Convert(CurrencyTradingPair pair)
        {
            return new BExError(ExchangeType.Bitfinex)
            {
                Message = message
            };
        }


    }
}