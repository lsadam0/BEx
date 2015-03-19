using Newtonsoft.Json;
using System;
using System.Globalization;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitStampUserTransactionJSON : IExchangeResponse//ExchangeResponse<UserTransaction>
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
                UserTransaction res = new UserTransaction(Conversion.ToDateTimeInvariant(Datetime), ExchangeType.BitStamp);

                res.BaseCurrencyAmount = Conversion.ToDecimalInvariant(Btc);
                res.CounterCurrencyAmount = Conversion.ToDecimalInvariant(Usd);
                res.TransactionId = Id;

                res.TradeFee = Conversion.ToDecimalInvariant(Fee);
                res.OrderId = (int)OrderId;
                res.TradeFeeCurrency = Currency.Unknown;

                return res;
            }
            else
                return null;
        }

        /*
        public override UserTransaction ConvertToStandard(CurrencyTradingPair pair)
        {
            if (OrderId != null && Type == 2)
            {
                UserTransaction res = new UserTransaction(Convert.ToDateTime(Datetime), ExchangeType.BitStamp);

                res.BaseCurrencyAmount = Conversion.ToDecimalInvariant(Btc);
                res.CounterCurrencyAmount = Conversion.ToDecimalInvariant(Usd);
                res.TransactionId = Id;

                res.TradeFee = Conversion.ToDecimalInvariant(Fee);
                res.OrderId = (int)OrderId;
                res.TradeFeeCurrency = Currency.Unknown;

                return res;
            }
            else
                return null;
        }*/
    }
}