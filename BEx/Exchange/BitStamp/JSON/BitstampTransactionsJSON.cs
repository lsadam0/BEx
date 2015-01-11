using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using BEx.Common;

namespace BEx.BitStampSupport
{
    /*
   internal class BitstampTransactionsJSON
    {
        public Class1[] Property1 { get; set; }
    }

    internal class Class1
    {
        public string date { get; set; }
        public int tid { get; set; }
        public string price { get; set; }
        public string amount { get; set; }
    }
    */
    public class BitstampTransactionJSON : ExchangeResponse
    {
        [JsonProperty("date")]
        public string date { get; set; }

        [JsonProperty("tid")]
        public int tid { get; set; }

        [JsonProperty("price")]
        public string price { get; set; }

        [JsonProperty("amount")]
        public string amount { get; set; }

        public override APIResult ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            Transaction res = new Transaction();

            res.Amount = Convert.ToDecimal(amount);
            res.Price = Convert.ToDecimal(price);
            res.TransactionID = Convert.ToInt64(tid);

            res.TimeStamp = UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(date));

            return res;
        }

        /*
        public static Transactions ConvertListToStandard(List<BitstampTransactionJSON> transactions, Currency baseCurrency, Currency counterCurrency)
        {
            Transactions res = new Transactions();

            foreach (BitstampTransactionJSON source in transactions)
            {
                res.TransactionsCollection.Add(source.ToTransaction());
            }

            return res;
        }*/
    }

}
