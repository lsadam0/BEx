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


        public Tick GetTick()
        {
            Tick res = null;

            res = new Tick(ExecuteCommand(APICommandCollection[(int)BitStampCommand.GetTick]));


            return res;

        }



        public BitStamp()
            : base()
        {
            BaseURI = new Uri(@"https://www.bitstamp.net/api/");
            LoadAPICommandCollection();

            Initialize();
        }


        protected override void LoadAPICommandCollection()
        {
            APICommandCollection = new Dictionary<int, APICommand>();

            APICommand tickCommand = new APICommand();

            tickCommand.HttpMethod = Method.GET;
            tickCommand.RelativeURI = @"ticker/";
            tickCommand.RequiresAuthentication = false;

            APICommandCollection.Add((int)BitStampCommand.GetTick, tickCommand);

        }

    }
}
