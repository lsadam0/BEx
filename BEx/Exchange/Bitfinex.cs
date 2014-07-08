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

        public Bitfinex() : base("BitFinex.xml")
        {

        }


        #region GetOrderBook

        public override OrderBook GetOrderBook()
        {
            return GetOrderBook(Currency.BTC, Currency.USD);
        }

        public override OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency)
        {
            OrderBook res;

            APICommand toExecute = APICommandCollection["OrderBook"];

            toExecute.BaseCurrency = baseCurrency;
            toExecute.CounterCurrency = counterCurrency;

            res = ExecuteCommand<BitFinexOrderBookJSON>(toExecute).ToOrderBook(baseCurrency, counterCurrency);

            return res;
        }

        #endregion

        #region GetTick

        /// <summary>
        /// Get the current BTC/USD tick
        /// </summary>
        /// <returns></returns>
        public override Tick GetTick()
        {
            return GetTick(Currency.BTC, Currency.USD);
        }

        /// <summary>
        /// Get the current tick for a particular currency pair
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="counterCurrency"></param>
        /// <returns></returns>
        public override Tick GetTick(Currency baseCurrency, Currency counterCurrency)
        {
            Tick res;

            APICommand toExecute = APICommandCollection["Tick"];

            toExecute.BaseCurrency = baseCurrency;
            toExecute.CounterCurrency = counterCurrency;

            res = ExecuteCommand<BitfinexTickJSON>(toExecute).ToTick(baseCurrency, counterCurrency);

            return res;

        }
        #endregion

        #region GetTransactions

        /// <summary>
        /// Get BTC/USD transactions for the previous hour
        /// </summary>
        /// <returns></returns>
        public override List<Transaction> GetTransactions()
        {
            return GetTransactions(Currency.BTC, Currency.USD);
        }

        /// <summary>
        /// Return transactions that have occurred since the provided DateTime
        /// </summary>
        /// <param name="sinceThisDate"></param>
        /// <returns></returns>
        public override List<Transaction> GetTransactions(Currency baseCurrency, Currency counterCurrency)
        {
            List<Transaction> res = new List<Transaction>();

            APICommand toExecute = APICommandCollection["Transactions"];

            toExecute.BaseCurrency = baseCurrency;
            toExecute.CounterCurrency = counterCurrency;

            DateTime sinceThisDate = DateTime.Now.AddHours(-1);
            toExecute.Parameters["timestamp"] = UnixTime.DateTimeToUnixTimestamp(sinceThisDate).ToString();

            List<BitFinexTransactionJSON> r = ExecuteCommand<List<BitFinexTransactionJSON>>(APICommandCollection["Transactions"]);

            return BitFinexTransactionJSON.ConvertBitFinexTransactionList(r, (Currency)toExecute.BaseCurrency, (Currency)toExecute.CounterCurrency);


        }

        #endregion




    }
}
