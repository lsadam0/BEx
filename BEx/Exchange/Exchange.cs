using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

using RestSharp;

namespace BEx
{
    public abstract class Exchange
    {

        protected RestClient apiClient
        {
            get;
            set;
        }

        protected RequestFactory apiRequestFactory
        {
            get;
            set;
        }


        public Uri BaseURI
        {

            get;
            set;
        }

        /// <summary>
        /// Consecutively increasing action counter
        /// </summary>
        /// <value>0</value>
        public long Nonce
        {
            get
            {
                return DateTime.Now.Ticks;
            }
        }

        public string Key
        {
            get;
            set;
        }

        public int Signature
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Dictionary<int, APICommand> APICommandCollection
        {
            // Serialize this into a config file?
            get;
            set;
        }


        public Exchange()
        {


        }

        internal void Initialize()
        {
            apiClient = new RestClient(BaseURI.ToString());
            apiRequestFactory = new RequestFactory(BaseURI);

        }

        public Tick GetTick()
        {
            ExecuteCommand(APICommandCollection[1]);

            return null;
        }

        public OrderBook GetOrderBook()
        {
            /*
             * 
             * BitStamp - GET https://www.bitstamp.net/api/order_book/
             * BTCe - TransHistory
             * GET /book/:symbol
             */
            throw new System.NotImplementedException();
        }

        public List<Transaction> GetTransactions()
        {
            throw new System.NotImplementedException();
        }

        public AccountBalance GetAccountBalance()
        {
            throw new System.NotImplementedException();
        }

        public List<BEx.Transaction> GetMyTransactions()
        {
            throw new System.NotImplementedException();
        }

        public List<Order> GetOpenOrders()
        {
            throw new System.NotImplementedException();
        }

        public void CancelOrder(Order toCancel)
        {
            throw new System.NotImplementedException();
        }

        protected BitstampTickJSON ExecuteCommand(APICommand toExecute)
        {

            RestRequest request = apiRequestFactory.GetRequest(toExecute);

            //IRestResponse response = apiClient.Execute(request);
            //var content = response.Content; // raw content as string


            //return apiClient.Execute<T>(request);
            IRestResponse<BitstampTickJSON> response = apiClient.Execute<BitstampTickJSON>(request);
            
            //apiClient.Execute<Bit
            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            //IRestResponse<Person> response2 = client.Execute<Person>(request);
            return response.Data;
        }

        protected object ExecutePOSTAction(APICommand toExecute)
        {
            return null;
        }

        protected object ExecuteGETAction(APICommand toExecute)
        {
            return null;

        }


        protected abstract void LoadAPICommandCollection();


    }
}
