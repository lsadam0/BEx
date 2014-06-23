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

        /// <summary>
        /// Get the BTC/USD Order Book
        /// </summary>
        /// <returns></returns>
        public OrderBook GetOrderBook()
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

        /// <summary>
        /// Return BTC/USD Transactions
        /// </summary>
        /// <returns></returns>
        public List<Transaction> GetTransactions()
        {
            return GetTransactions(Currency.BTC, Currency.USD);
        }


        /// <summary>
        /// Get Transactions for a particular time frame
        /// </summary>
        /// <param name="time">Accepted values: "minute" or "hour"</param>
        /// <returns></returns>
        public List<Transaction> GetTransactions(string timeFrame)
        {
            List<Transaction> res = new List<Transaction>();

            APICommand toExecute = APICommandCollection["Transactions"];

            toExecute.Parameters["time"] = timeFrame;

            List<BitstampTransactionJSON> r = ExecuteCommand<List<BitstampTransactionJSON>>(toExecute);
            
            return BitstampTransactionJSON.ConvertBitStampTransactionList(r, Currency.BTC, Currency.USD);
        }

        public override List<Transaction> GetTransactions(Currency baseCurrency, Currency counterCurrency)
        {
            List<Transaction> res = new List<Transaction>();

            List<BitstampTransactionJSON> r = ExecuteCommand<List<BitstampTransactionJSON>>(APICommandCollection["Transactions"]);

            return BitstampTransactionJSON.ConvertBitStampTransactionList(r, baseCurrency, counterCurrency);
            

        }


        /// <summary>
        /// Get the current BTC/USD Tick
        /// </summary>
        /// <returns>Tick</returns>
        public Tick GetTick()
        {
            return GetTick(Currency.BTC, Currency.USD);
        }

        public override Tick GetTick(Currency baseCurrency, Currency counterCurrency)
        {
            Tick res;

            APICommand toExecute = APICommandCollection["Tick"];

            toExecute.BaseCurrency = Currency.BTC;
            toExecute.CounterCurrency = Currency.USD;

            res = ExecuteCommand<BitstampTickJSON>(toExecute).ToBitStampTick((Currency)toExecute.BaseCurrency, (Currency)toExecute.CounterCurrency);

            return res;
        }
        


    }
}
