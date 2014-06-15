using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BEx.BitFinexSupport;

namespace BEx
{
    public class Bitfinex : Exchange
    {

        public Bitfinex() : base("BitFinex.xml")
        {

        }

        public override Tick GetTick()
        {
            Tick res;

            APICommand toExecute = APICommandCollection["Tick"];

            toExecute.BaseCurrency = Currency.BTC;
            toExecute.CounterCurrency = Currency.USD;

            res = new Tick(ExecuteCommand<BitfinexTickJSON>(toExecute), Currency.BTC, Currency.USD);

            return res;
        }

        public Tick GetTick(Currency baseCurrency, Currency counterCurrency)
        {
            Tick res;

            APICommand toExecute = APICommandCollection["Tick"];

            toExecute.BaseCurrency = baseCurrency;
            toExecute.CounterCurrency = counterCurrency;

            res = new Tick(ExecuteCommand<BitfinexTickJSON>(toExecute), baseCurrency, counterCurrency);

            return res;

        }

        public override OrderBook GetOrderBook()
        {
            OrderBook res;

            APICommand toExecute = APICommandCollection["OrderBook"];

            toExecute.BaseCurrency = Currency.BTC;
            toExecute.CounterCurrency = Currency.USD;

            res = new OrderBook(ExecuteCommand<BitFinexOrderBookJSON>(toExecute), Currency.BTC, Currency.USD);

            return res;
        }

        public override List<Transaction> GetTransactions()
        {
            List<Transaction> res = new List<Transaction>();

            List<BitFinexTransactionJSON> r = ExecuteCommand<List<BitFinexTransactionJSON>>(APICommandCollection["Transactions"]);

            
            return Transaction.ConvertBitStampTransactionList(r);
        }
    }
}
