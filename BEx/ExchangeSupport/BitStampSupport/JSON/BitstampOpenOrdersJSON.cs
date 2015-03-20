using Newtonsoft.Json;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitStampOpenOrdersJSON : IExchangeResponse
    {
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("datetime")]
        public string Datetime { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            return new Order(Conversion.ToDateTimeInvariant(Datetime), ExchangeType.BitStamp)
            {
                Amount = Conversion.ToDecimalInvariant(Amount),
                Price = Conversion.ToDecimalInvariant(Price),
                TradeType = Type == 0 ? OrderType.Buy : OrderType.Sell,
                Id = Id,
                ExchangeTimestamp = Conversion.ToDateTimeInvariant(Datetime),
                Pair = pair
            };
        }
    }
}