using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BEx.BTCeSupport;

namespace BEx
{
    public class BTCe : Exchange
    {
        public BTCe() : base("BTCe.xml")
        {
            

        }


        #region GetOrderBook

        public override OrderBook GetOrderBook()
        {
            return GetOrderBook(Currency.BTC, Currency.USD);
        }

        public override OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency)
        {

            BTCeAPICommand toExecute = new BTCeAPICommand(APICommandCollection["OrderBook"]);

            toExecute.BaseCurrency = baseCurrency;
            toExecute.CounterCurrency = counterCurrency;

            BTCeOrderBookJSON r = ExecuteCommand<BTCeOrderBookJSON>(toExecute);

            return r.ToOrderBook(baseCurrency, counterCurrency);

        }

        #endregion

        #region GetTick

        /// <summary>
        /// Get the current tick for a particular currency pair
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="counterCurrency"></param>
        /// <returns></returns>
        public override Tick GetTick(Currency baseCurrency, Currency counterCurrency)
        {
                Tick res;

                BTCeAPICommand tickCommand = new BTCeAPICommand(APICommandCollection["Tick"]);

                tickCommand.BaseCurrency = baseCurrency;
                tickCommand.CounterCurrency = counterCurrency;

                res = ExecuteCommand<BTCeTickJSON>(tickCommand).ToTick(baseCurrency, counterCurrency);

                return res;
        }

        /// <summary>
        /// Return the current BTC/USD Tick
        /// </summary>
        /// <returns></returns>
        public override Tick GetTick()
        {
            return GetTick(Currency.BTC, Currency.USD);
        }

        #endregion

        #region GetTransactions

        public override List<Transaction> GetTransactions()
        {
            return GetTransactions(Currency.BTC, Currency.USD);
        }

        public override List<Transaction> GetTransactions(Currency baseCurrency, Currency counterCurrency)
        {
            

            BTCeAPICommand toExecute = new BTCeAPICommand(APICommandCollection["Transactions"]);

            double since = Common.UnixTime.DateTimeToUnixTimestamp(DateTime.Now.AddHours(-1));
            toExecute.Parameters["since"] = since.ToString();
            toExecute.BaseCurrency = baseCurrency;
            toExecute.CounterCurrency = counterCurrency;

            List<BTCeTransactionsJSON> r = ExecuteCommand<List<BTCeTransactionsJSON>>(toExecute);

            return BTCeTransactionsJSON.ConvertBTCeTransactionList(r, baseCurrency, counterCurrency);

        }

        #endregion

    }
}
