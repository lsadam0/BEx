using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using RestSharp;
using BEx.BitStampSupport;

namespace BEx
{
    public class BitStamp : Exchange
    {

        public BitStamp()
            : base("Bitstamp.xml")
        {
            
            
        }


        #region GetOrderBook

        /// <summary>
        /// Get the BTC/USD Order Book
        /// </summary>
        /// <returns></returns>
        public override OrderBook GetOrderBook()
        {
            return GetOrderBook(Currency.BTC, Currency.USD);
        }

        public override OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency)
        {
            OrderBook res;

            APICommand toExecute = APICommandCollection["OrderBook"];

            res = ExecuteCommand<BitstampOrderBookJSON>(toExecute).ToOrderbook(Currency.BTC, Currency.USD);

            return res;
        }

        #endregion

        #region GetTransactions
        /// <summary>
        /// Return BTC/USD Transactions for the previous hour
        /// </summary>
        /// <returns></returns>
        public override List<Transaction> GetTransactions()
        {
            return GetTransactions(Currency.BTC, Currency.USD);
        }
       
        /// <summary>
        /// Return transactions from the previous hour for a particular currency pair
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="counterCurrency"></param>
        /// <returns></returns>
        public override List<Transaction> GetTransactions(Currency baseCurrency, Currency counterCurrency)
        {
            List<Transaction> res = new List<Transaction>();

            List<BitstampTransactionJSON> r = ExecuteCommand<List<BitstampTransactionJSON>>(APICommandCollection["Transactions"]);

            return BitstampTransactionJSON.ConvertBitStampTransactionList(r, baseCurrency, counterCurrency);
            

        }
        #endregion

        #region GetTick
        /// <summary>
        /// Get the current BTC/USD Tick
        /// </summary>
        /// <returns>Tick</returns>
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

            toExecute.BaseCurrency = Currency.BTC;
            toExecute.CounterCurrency = Currency.USD;

            res = ExecuteCommand<BitstampTickJSON>(toExecute).ToBitStampTick((Currency)toExecute.BaseCurrency, (Currency)toExecute.CounterCurrency);

            return res;
        }
        #endregion



    }
}
