using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

using RestSharp;

namespace BEx
{
    public abstract class Exchange
    {

        protected Dictionary<Currency, Currency> SupportedPairs = new Dictionary<Currency, Currency>();


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

        public Dictionary<string, APICommand> APICommandCollection
        {
            // Serialize this into a config file?
            get;
            set;
        }

        public Exchange(string configFile)
        {
            LoadConfigFromXML(configFile);

            Initialize();
        }

        internal void Initialize()
        {
            apiClient = new RestClient(BaseURI.ToString());
            apiRequestFactory = new RequestFactory(BaseURI);

        }



        protected void LoadConfigFromXML(string file)
        {
            APICommandCollection = new Dictionary<string, APICommand>();

            XElement configFile = XElement.Load(file);

            string baseUrl = configFile.Element("BaseURL").Value;

            BaseURI = new Uri(baseUrl);

            XElement supportedPairs = configFile.Element("SupportedPairs");
            SupportedPairs = new Dictionary<Currency, Currency>();
            foreach (XElement pairs in supportedPairs.Elements())
            {
                XElement b = pairs.Element("Base");

                XElement c = pairs.Element("Counter");

                Currency bs;
                Currency cs;

                if (Enum.TryParse<Currency>(b.Value, out bs)
                    &&
                Enum.TryParse<Currency>(c.Value, out cs))
                {
                    SupportedPairs.Add(bs, cs);
                }

              //  Enum.TryParse<Currency>(pair)
            }

            XElement commands = configFile.Element("Commands");

            foreach (XElement command in commands.Elements())
            {
                APICommand commandToLoad = new APICommand();

                foreach (XElement c in command.Elements())
                {

                    if (c.Name == "Method")
                    {
                        if (c.Value == "GET")
                        {
                            commandToLoad.HttpMethod = Method.GET;
                        }
                        else
                            commandToLoad.HttpMethod = Method.POST;
                    }

                    if (c.Name == "RelativeURL")
                    {
                        commandToLoad.RelativeURI = c.Value;
                    }

                    if (c.Value == "RequiresAuthentication")
                    {
                        commandToLoad.RequiresAuthentication = Convert.ToBoolean(c.Value);
                    }

                }

                string cId = command.Attribute("ID").Value;

                APICommandCollection.Add(cId, commandToLoad);

            }

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

        protected T ExecuteCommand<T>(APICommand toExecute) where T : new()
        {
            RestRequest request = apiRequestFactory.GetRequest(toExecute);

            IRestResponse<T> response = apiClient.Execute<T>(request);

            return (T)response.Data;
        }


        protected string ExecuteCommand(APICommand toExecute)
        {
            RestRequest request = apiRequestFactory.GetRequest(toExecute);

            IRestResponse response = apiClient.Execute(request);

            return response.Content;
        }


        protected object ExecutePOSTAction(APICommand toExecute)
        {
            return null;
        }

        protected object ExecuteGETAction(APICommand toExecute)
        {
            return null;

        }


        public abstract Tick GetTick();

        public abstract OrderBook GetOrderBook();

        public abstract List<Transaction> GetTransactions();
    }
}
