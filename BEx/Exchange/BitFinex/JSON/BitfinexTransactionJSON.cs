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
            Transaction res = new Transaction();

            res.Amount = Convert.ToDecimal(amount);
            res.Price = Convert.ToDecimal(price);
            res.TransactionID = Convert.ToInt64(tid);

            res.TimeStamp = UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(timestamp));

            return res;
        }
        /*
        public Transaction ToTransaction()
        {
            Transaction res = new Transaction();

            res.Amount = Convert.ToDecimal(amount);
            res.Price = Convert.ToDecimal(price);
            res.TransactionID = Convert.ToInt64(tid);

            res.TimeStamp = UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(timestamp));

            return res;

        }

        public static Transactions ConvertListToStandard(List<BitFinexTransactionJSON> transactions, Currency baseCurrency, Currency counterCurrency)
        {
            Transactions res = new Transactions();

            foreach (BitFinexTransactionJSON source in transactions)
            {
                res.TransactionsCollection.Add(source.ToTransaction());
            }

            return res;
        }*/
    }
}
