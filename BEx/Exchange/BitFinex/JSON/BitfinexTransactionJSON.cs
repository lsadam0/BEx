using BEx.Common;
using Newtonsoft.Json;
using System;

namespace BEx.BitFinexSupport
{
    public class BitFinexTransactionJSON : ExchangeResponse<Transaction>
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

        public override Transaction ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            Transaction res = new Transaction(UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(timestamp)));

            res.Amount = Convert.ToDecimal(amount);
            res.Price = Convert.ToDecimal(price);
            res.TransactionID = Convert.ToInt64(tid);

            return res;
        }

       
    }
}