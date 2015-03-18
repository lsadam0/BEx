using BEx.CommandProcessing;
using Newtonsoft.Json;
using System;

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

        public APIResult ConvertToStandard(CurrencyTradingPair pair)
        {
            UserTransaction u = new UserTransaction(UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(Timestamp)), ExchangeType.BitFinex);

            u.TransactionId = Tid;
            u.OrderId = Tid;

            u.BaseCurrencyAmount = 0;
            u.CounterCurrencyAmount = 0;

            u.TradeFee = Convert.ToDecimal(FeeAmount);
            u.TradeFeeCurrency = Currency.Unknown;

            return u;
        }
    }
}