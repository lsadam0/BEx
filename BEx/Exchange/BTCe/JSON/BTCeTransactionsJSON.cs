using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using BEx.Common;

namespace BEx.BTCeSupport
{
    public class BTCeTransactionsJSON
    {
        [JsonProperty("date")]
        public int Date { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("tid")]
        public int Tid { get; set; }

        [JsonProperty("price_currency")]
        public string PriceCurrency { get; set; }

        [JsonProperty("item")]
        public string Item { get; set; }

        [JsonProperty("trade_type")]
        public string TradeType { get; set; }


        public Transaction ToTransaction()
        {
            Transaction res = new Transaction();

            res.Amount = Convert.ToDecimal(Amount);
            res.Price = Convert.ToDecimal(Price);
            res.TimeStamp = UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(Date));
            res.TransactionID = Convert.ToInt64(Tid);

            return res;
        }

        public static Transactions ConvertListToStandard(List<BTCeTransactionsJSON> transactions, Currency baseCurrency, Currency counterCurrency)
        {
            Transactions res = new Transactions();

            foreach (BTCeTransactionsJSON source in transactions)
            {
                res.TransactionsCollection.Add(source.ToTransaction());
            }

            return res;

        }
    }
}
