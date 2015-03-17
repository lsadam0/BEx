using BEx.CommandProcessing;
using Newtonsoft.Json;
using System;

namespace BEx.ExchangeSupport.BitfinexSupport
{
    internal class BitFinexTransactionJSON : ExchangeResponse<Transaction>
    {
        [JsonProperty("timestamp")]
        public int timestamp { get; set; }

        [JsonProperty("tid")]
        public int tid { get; set; }

        [JsonProperty("price")]
        public string price { get; set; }

        [JsonProperty("amount")]
        public string amount { get; set; }

        [JsonProperty("exchange")]
        public string exchange { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        public override Transaction ConvertToStandard(CurrencyTradingPair pair)
        {
            Transaction res = new Transaction(DateTime.Now, ExchangeType.BitFinex);

            res.Amount = Convert.ToDecimal(amount);
            res.Price = Convert.ToDecimal(price);
            res.TransactionID = Convert.ToInt64(tid);
            res.CompletedTime = UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(timestamp));
            res.Pair = pair;

            return res;
        }
    }
}