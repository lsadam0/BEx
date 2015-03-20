using Newtonsoft.Json;

namespace BEx.ExchangeSupport.BitfinexSupport
{
    internal class BitFinexUserTransactionJSON : IExchangeResponse
    {
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("fee_currency")]
        public string FeeCurrency { get; set; }

        [JsonProperty("fee_amount")]
        public string FeeAmount { get; set; }

        [JsonProperty("tid")]
        public int Tid { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            return new UserTransaction(UnixTime.UnixTimeStampToDateTime(Conversion.ToDoubleInvariant(Timestamp)), ExchangeType.Bitfinex)
            {
                TransactionId = Tid,
                OrderId = Tid,
                BaseCurrencyAmount = 0,
                CounterCurrencyAmount = 0,
                TradeFee = Conversion.ToDecimalInvariant(FeeAmount),
                TradeFeeCurrency = Currency.Unknown
            };
        }
    }
}