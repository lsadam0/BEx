using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BEx.BitFinexSupport;
using BEx.Common;

namespace BEx
{
    public class Bitfinex : Exchange
    {

        public Bitfinex()
            : base("BitFinex.xml")
        {

        }



        public override Tick GetTick()
        {
            return base.GetTick<BitfinexTickJSON>(Currency.BTC, Currency.USD);
        }

        public override Tick GetTick(Currency baseCurrency, Currency counterCurrency)
        {
            return base.GetTick<BitfinexTickJSON>(baseCurrency, counterCurrency);
        }

        public override OrderBook GetOrderBook()
        {

            return base.GetOrderBook<BitFinexOrderBookJSON>(Currency.BTC, Currency.USD);
        }

        public override OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency)
        {
            return base.GetOrderBook<BitFinexOrderBookJSON>(baseCurrency, counterCurrency);
        }

        public override List<Transaction> GetTransactions()
        {
            return base.GetTransactions<BitFinexTransactionJSON>(Currency.BTC, Currency.USD);
        }

        public override List<Transaction> GetTransactions(Currency baseCurrency, Currency counterCurrency)
        {
            return base.GetTransactions<BitFinexTransactionJSON>(baseCurrency, counterCurrency);
        }


        public override object GetAccountBalance()
        {
            return base.GetAccountBalance(Currency.BTC, Currency.USD);
        }


        protected override Tuple<string, string, string> CreateSignature()
        {
            Tuple<string, string, string> res = new Tuple<string, string, string>("", "", "");

            return res;
        }

        internal override void SetParameters(APICommand command)
        {
            /*
            switch (command.ID)
            {
                case "Transactions":
                    DateTime sinceThisDate = DateTime.Now.AddHours(-1);
                    command.Parameters["timestamp"] = UnixTime.DateTimeToUnixTimestamp(sinceThisDate).ToString();
                    break;
                default:
                    break;
            }*/
        }
    }
}
