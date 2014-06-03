using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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


        

        public BitStamp() : base()
        {
            LoadAPICommandCollection();
        }


        protected override void LoadAPICommandCollection()
        {

            APICommand tickCommand = new APICommand();

            tickCommand.HttpMethod = Method.GET;
            tickCommand.RelativeURI = @"ticker/";
            tickCommand.RequiresAuthentication = false;

            APICommandCollection.Add(1, tickCommand);
            
        }
        
    }
}
