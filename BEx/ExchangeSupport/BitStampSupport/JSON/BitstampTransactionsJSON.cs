using BEx.CommandProcessing;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitstampTransactionJSON : IExchangeResponse//ExchangeResponse<Transaction>
    {
        [JsonProperty("date")]
        public string date { get; set; }

        [JsonProperty("tid")]
        public int tid { get; set; }

        [JsonProperty("price")]
        public string price { get; set; }

        [JsonProperty("amount")]
        public string amount { get; set; }

        public ApiResult ConvertToStandard(CurrencyTradingPair pair)
        {
            DateTime time = UnixTime.UnixTimeStampToDateTime(Conversion.ToDoubleInvariant(date));
            Transaction res = new Transaction(DateTime.Now, ExchangeType.BitStamp);

            res.Amount = Conversion.ToDecimalInvariant(amount);
            res.Price = Conversion.ToDecimalInvariant(price);
            res.TransactionId = (long)tid;
            res.Pair = pair;
            res.CompletedTime = time;

            return res;
        }
    }
}