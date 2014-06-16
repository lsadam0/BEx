using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

using RestSharp;
using BEx.BitStampSupport;

namespace BEx
{
    public class BitStamp : Exchange
    {
        public void CreateBuyLimitOrder()
        {
            throw new System.NotImplementedException();
        }

        public void CreateSellLimitOrder()
        {
            throw new System.NotImplementedException();
        }

        public void GetUnconfirmedDeposits()
        {
            throw new System.NotImplementedException();
        }

        public void CreateWithDrawalRequest()
        {
            throw new System.NotImplementedException();
        }

        public void GetDepositAddress()
        {
            throw new System.NotImplementedException();
        }

        public BitStamp()
            : base("Bitstamp.xml")
        {
            
            
        }

        /// <summary>
        /// Return the current tick for the BTC/USD pair
        /// </summary>
        /// <returns></returns>
        public override Tick GetTick()
        {
            Tick res;

            APICommand toExecute = APICommandCollection["Tick"];

            toExecute.BaseCurrency = Currency.BTC;
            toExecute.CounterCurrency = Currency.USD;

            res = new BitstampTick(ExecuteCommand<BitstampTickJSON>(toExecute), Currency.BTC, Currency.USD);

            return res;
        }

        public override OrderBook GetOrderBook()
        {
            OrderBook res;

            APICommand toExecute = APICommandCollection["OrderBook"];

            res = new OrderBook(ExecuteCommand<BitstampOrderBookJSON>(toExecute), Currency.BTC, Currency.USD);

            return res;
        }

        public List<Transaction> GetTransactions(string time)
        {
            List<Transaction> res = new List<Transaction>();

            APICommand toExecute = APICommandCollection["Transactions"];

            toExecute.Parameters["time"] = time;

            List<BitstampTransactionJSON> r = ExecuteCommand<List<BitstampTransactionJSON>>(toExecute);

            return Transaction.ConvertBitStampTransactionList(r);           

        }

        public override List<Transaction> GetTransactions()
        {
            List<Transaction> res = new List<Transaction>();

            List<BitstampTransactionJSON> r = ExecuteCommand<List<BitstampTransactionJSON>>(APICommandCollection["Transactions"]);

            return Transaction.ConvertBitStampTransactionList(r);

        }
        


    }
}
