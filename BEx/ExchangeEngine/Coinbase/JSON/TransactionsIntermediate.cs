using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEx.ExchangeEngine.Utilities;

namespace BEx.ExchangeEngine.Coinbase.JSON
{
    /*

    public class TransactionsIntermediate : IExchangeResponseIntermediate<Transactions>
    {
        public TransactionIntermediate[] Property1 { get; set; }

        public Transactions Convert(TradingPair pair)
        {

         

            return new Transactions(
                Property1.Select(x => x.Convert(pair)),
                pair,
                ExchangeType.Coinbase);



        }
    }
    */
    public class TransactionIntermediate : IExchangeResponseIntermediate<Transaction>
    {
        public DateTime time { get; set; }
        public int trade_id { get; set; }
        public string price { get; set; }
        public string size { get; set; }
        public string side { get; set; }

        public Transaction Convert(TradingPair pair)
        {
            return new Transaction(
                size,
                pair,
                new DateTime(time.Ticks, DateTimeKind.Utc),
                trade_id,
                price,
                ExchangeType.Coinbase);

        }
    }

}