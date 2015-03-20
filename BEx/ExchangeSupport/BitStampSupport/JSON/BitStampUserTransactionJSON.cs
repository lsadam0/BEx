using Newtonsoft.Json;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitStampUserTransactionJSON : IExchangeResponse
    {
        [JsonProperty("usd")]
        public string Usd { get; set; }

        [JsonProperty("btc")]
        public string Btc { get; set; }

        [JsonProperty("btc_usd")]
        public string BtcUsd { get; set; }

        [JsonProperty("order_id")]
        public int? OrderId { get; set; }

        [JsonProperty("fee")]
        public string Fee { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("datetime")]
        public string Datetime { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            if (OrderId != null && Type == 2)
            {
                return new UserTransaction(Conversion.ToDateTimeInvariant(Datetime), ExchangeType.BitStamp)
                {
                    BaseCurrencyAmount = Conversion.ToDecimalInvariant(Btc),
                    CounterCurrencyAmount = Conversion.ToDecimalInvariant(Usd),
                    OrderId = (int)OrderId,
                    TradeFee = Conversion.ToDecimalInvariant(Fee),
                    TradeFeeCurrency = Currency.Unknown,
                    TransactionId = Id
                };
            }
            else
                return null;
        }
    }
}