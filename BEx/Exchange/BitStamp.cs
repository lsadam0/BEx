using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

using RestSharp;

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

        public override Tick GetTick()
        {
            Tick res;

            res = new Tick(ExecuteCommand<BitstampTickJSON>(APICommandCollection["Tick"]));

            return res;
        }

        public override OrderBook GetOrderBook()
        {
            OrderBook res;

            //res = new OrderBook(ExecuteCommand<BitstampOrderBookJSON>(APICommandCollection["OrderBook"]));

            string response = ExecuteCommand(APICommandCollection["OrderBook"]);

            return null;
        }

        public override List<Transaction> GetTransactions()
        {
            List<Transaction> res = new List<Transaction>();


            BitstampTransactionsJSON r = ExecuteCommand<BitstampTransactionsJSON>(APICommandCollection["Transactions"]);

            return null;
        }
        


    }
}
