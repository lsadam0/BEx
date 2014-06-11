using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using RestSharp;

namespace BEx
{
    public abstract class Exchange
    {

        //protected Dictionary<Currency, Currency> SupportedPairs = new Dictionary<Currency, Currency>();

        protected Dictionary<Currency, HashSet<Currency>> SupportedPairs;
        
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

        private void LoadSupportedPairs(XElement configFile)
        {
            XElement supportedPairs = configFile.Element("SupportedPairs");

            SupportedPairs = new Dictionary<Currency, HashSet<Currency>>();
            
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
                    if (!SupportedPairs.ContainsKey(bs))
                    {
                        SupportedPairs.Add(bs, new HashSet<Currency>());
                    }

                    if (!SupportedPairs[bs].Contains(cs))
                    {
                        SupportedPairs[bs].Add(cs);
                    }
                }

            }
        }

        protected void LoadCommands(XElement configFile)
        {
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

        protected void LoadConfigFromXML(string file)
        {
            APICommandCollection = new Dictionary<string, APICommand>();

            XElement configFile = XElement.Load(file);

            string baseUrl = configFile.Element("BaseURL").Value;

            BaseURI = new Uri(baseUrl);

            LoadSupportedPairs(configFile);

            LoadCommands(configFile);

            

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
            

            if (toExecute.BaseCurrency != null && toExecute.CounterCurrency != null)
            {
                if (!IsCurrencyPairSupported((Currency)toExecute.BaseCurrency, (Currency)toExecute.CounterCurrency))
                {
                    throw new NotImplementedException(string.Format(ErrorMessages.UnsupportedCurrencyPair, toExecute.BaseCurrency.ToString(), toExecute.CounterCurrency.ToString(), this.GetType().ToString()));
                }
                
            }

            RestRequest request = apiRequestFactory.GetRequest(toExecute);

            IRestResponse response = apiClient.Execute(request);

            return JsonConvert.DeserializeObject<T>(response.Content);
            //return (T)response.Data;
        }

        private bool IsCurrencyPairSupported(Currency baseC, Currency counterC)
        {
            bool res = false;

            if (SupportedPairs.ContainsKey(baseC))
            {
                if (SupportedPairs[baseC].Contains(counterC))
                {
                    res = true;
                }
            }

            return res;
        }
        /*
        protected string ExecuteCommand(APICommand toExecute)
        {
            RestRequest request = apiRequestFactory.GetRequest(toExecute);

            IRestResponse response = apiClient.Execute(request);

            return response.Content;
        }*/


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
