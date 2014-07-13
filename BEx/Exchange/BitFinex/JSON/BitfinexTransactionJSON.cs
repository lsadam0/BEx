using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using BEx.Common;

namespace BEx.BitFinexSupport
{
    public class BitFinexTransactionJSON
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


        public Transaction ToTransaction()
        {
            Transaction res = new Transaction();

            res.Amount = Convert.ToDecimal(amount);
            res.Price = Convert.ToDecimal(price);
            res.TransactionID = Convert.ToInt64(tid);

            res.TimeStamp = UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(timestamp));

            return res;

        }

        public static List<Transaction> ConvertToStandard(List<BitFinexTransactionJSON> transactions, Currency baseCurrency, Currency counterCurrency)
        {
            List<Transaction> res = new List<Transaction>();

            foreach (BitFinexTransactionJSON source in transactions)
            {
                res.Add(source.ToTransaction());
            }

            return res;
        }
    }
}
