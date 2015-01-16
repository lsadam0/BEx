using BEx.Common;
using Newtonsoft.Json;
using System;

namespace BEx.BTCeSupport
{
    public class BTCeTransactionsJSON : ExchangeResponse<Transaction>
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


        public override Transaction ConvertToStandard(Currency baseCurrency, Currency counterCurrency)
        {
            Transaction res = new Transaction();

            res.Amount = Convert.ToDecimal(Amount);
            res.Price = Convert.ToDecimal(Price);
            res.TimeStamp = UnixTime.UnixTimeStampToDateTime(Convert.ToDouble(Date));
            res.TransactionID = Convert.ToInt64(Tid);

            return res;
        }
        /*
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

        }*/
    }
}
