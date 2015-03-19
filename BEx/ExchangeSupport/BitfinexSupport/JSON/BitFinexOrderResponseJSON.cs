using BEx.CommandProcessing;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace BEx.ExchangeSupport.BitfinexSupport
{
    internal class BitFinexOrderResponseJSON : IExchangeResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("avg_execution_price")]
        public string AvgExecutionPrice { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("is_live")]
        public bool IsLive { get; set; }

        [JsonProperty("is_cancelled")]
        public bool IsCancelled { get; set; }

        [JsonProperty("was_forced")]
        public bool WasForced { get; set; }

        [JsonProperty("original_amount")]
        public string OriginalAmount { get; set; }

        [JsonProperty("remaining_amount")]
        public string RemainingAmount { get; set; }

        [JsonProperty("executed_amount")]
        public string ExecutedAmount { get; set; }

        [JsonProperty("order_id")]
        public int OrderId { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            Order res = new Order(UnixTime.UnixTimeStampToDateTime(Timestamp), ExchangeType.Bitfinex);

            res.Amount = Convert.ToDecimal(OriginalAmount, CultureInfo.InvariantCulture);
            res.Pair = pair;
            res.Id = Id;
            res.Price = Convert.ToDecimal(Price, CultureInfo.InvariantCulture);

            return res;
        }
    }
}