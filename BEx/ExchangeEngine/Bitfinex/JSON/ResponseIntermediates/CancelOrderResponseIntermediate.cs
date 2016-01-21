using BEx.ExchangeEngine.Utilities;
using Newtonsoft.Json;

namespace BEx.ExchangeEngine.Bitfinex.JSON.ResponseIntermediates
{
    internal class CancelOrderResponseIntermediate : IExchangeResponse<Confirmation>
    {
        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("symbol", Required = Required.Always)]
        public string Symbol { get; set; }

        [JsonProperty("exchange", Required = Required.Always)]
        public string Exchange { get; set; }

        [JsonProperty("price", Required = Required.Always)]
        public string Price { get; set; }

        [JsonProperty("avg_execution_price", Required = Required.Always)]
        public string AvgExecutionPrice { get; set; }

        [JsonProperty("side", Required = Required.Always)]
        public string Side { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
        public string Timestamp { get; set; }

        [JsonProperty("is_live", Required = Required.Always)]
        public bool IsLive { get; set; }

        [JsonProperty("is_cancelled", Required = Required.Always)]
        public bool IsCancelled { get; set; }

        [JsonProperty("was_forced", Required = Required.Always)]
        public bool WasForced { get; set; }

        [JsonProperty("original_amount", Required = Required.Always)]
        public string OriginalAmount { get; set; }

        [JsonProperty("remaining_amount", Required = Required.Always)]
        public string RemainingAmount { get; set; }

        [JsonProperty("executed_amount", Required = Required.Always)]
        public string ExecutedAmount { get; set; }

        public int OrderId { get; set; }

        public Confirmation Convert(TradingPair pair)
        {
            return new Confirmation(Timestamp.ToDateTimeUTC(), ExchangeType.Bitfinex)
            {
                IsConfirmed = true
            };
        }
    }
}