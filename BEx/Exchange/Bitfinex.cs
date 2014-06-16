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

        public OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency)
        {
            OrderBook res;

            APICommand toExecute = APICommandCollection["OrderBook"];

            toExecute.BaseCurrency = baseCurrency;
            toExecute.CounterCurrency = counterCurrency;

            res = new OrderBook(ExecuteCommand<BitFinexOrderBookJSON>(toExecute), baseCurrency, counterCurrency);

            return res;
        }

        public override List<Transaction> GetTransactions()
        {
            List<Transaction> res = new List<Transaction>();

            APICommand toExecute = APICommandCollection["Transactions"];

            toExecute.BaseCurrency = Currency.BTC;
            toExecute.CounterCurrency = Currency.USD;

            List<BitFinexTransactionJSON> r = ExecuteCommand<List<BitFinexTransactionJSON>>(APICommandCollection["Transactions"]);

            
            return Transaction.ConvertBitFinexTransactionList(r);
        }

        public List<Transaction> GetTransactions(Currency baseC, Currency counterC)
        {
            List<Transaction> res = new List<Transaction>();

            APICommand toExecute = APICommandCollection["Transactions"];

            toExecute.BaseCurrency = baseC;
            toExecute.CounterCurrency = counterC;

            List<BitFinexTransactionJSON> r = ExecuteCommand<List<BitFinexTransactionJSON>>(APICommandCollection["Transactions"]);


            return Transaction.ConvertBitFinexTransactionList(r);
        }
    }
}
