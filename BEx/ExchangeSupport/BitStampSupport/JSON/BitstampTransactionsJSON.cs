using BEx.CommandProcessing;
using Newtonsoft.Json;
using System;

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

        public APIResult ConvertToStandard(CurrencyTradingPair pair)
        {
            DateTime time = UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(date));
            Transaction res = new Transaction(DateTime.Now, ExchangeType.BitStamp);

            res.Amount = Convert.ToDecimal(amount);
            res.Price = Convert.ToDecimal(price);
            res.TransactionID = Convert.ToInt64(tid);
            res.Pair = pair;
            res.CompletedTime = time;

            return res;
        }
    }
}