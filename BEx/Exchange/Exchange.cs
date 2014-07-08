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
using BEx.BitFinexSupport;

namespace BEx
{
    public abstract class Exchange<K, A>
    {

        #region Vars
        /// <summary>
        /// Collection of currency pairs supported by the current exchange indexed by the base currency
        /// </summary>
        public Dictionary<Currency, HashSet<Currency>> SupportedPairs;

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

        protected Dictionary<string, APICommand> APICommandCollection
        {
            get;
            set;
        }

        #endregion

        protected Exchange(string configFile)
        {
            LoadConfigFromXML(configFile);

            apiClient = new RestClient(BaseURI.ToString());
            apiRequestFactory = new RequestFactory(BaseURI);
        }

        #region Command Execution
        /// <summary>
        /// Verify that a currency pair (e.g. BTC/USD) is supported by this exchange.
        /// </summary>
        /// <param name="baseCurrency">Base Currency</param>
        /// <param name="counterCurrency">Counter Currency</param>
        /// <returns>True if supported, otherwise false.</returns>
        public bool IsCurrencyPairSupported(Currency baseCurrency, Currency counterCurrency)
        {
            bool res = false;

            if (SupportedPairs.ContainsKey(baseCurrency))
            {
                if (SupportedPairs[baseCurrency].Contains(counterCurrency))
                {
                    res = true;
                }
            }

            return res;
        }

        protected void VerifyCurrencySupport(APICommand toCheck)
        {
            if (toCheck.BaseCurrency != null && toCheck.CounterCurrency != null)
            {
                if (!IsCurrencyPairSupported((Currency)toCheck.BaseCurrency, (Currency)toCheck.CounterCurrency))
                {
                    throw new NotImplementedException(string.Format(ErrorMessages.UnsupportedCurrencyPair, toCheck.BaseCurrency.ToString(), toCheck.CounterCurrency.ToString(), this.GetType().ToString()));
                }
            }
        }
        /*
        protected T ExecuteCommand<T>(APICommand toExecute) where T : new()
        {
            VerifyCurrencySupport(toExecute);

            RestRequest request = apiRequestFactory.GetRequest(toExecute);

            IRestResponse response = apiClient.Execute(request);
            
            return JsonConvert.DeserializeObject<T>(response.Content);
        }*/

        protected string ExecuteCommand(APICommand toExecute)
        {
            VerifyCurrencySupport(toExecute);

            RestRequest request = apiRequestFactory.GetRequest(toExecute);

            IRestResponse response = apiClient.Execute(request);

            return response.Content;
        }

        #endregion

        #region Load Exchange from XML

        protected void LoadCommands(XElement configFile)
        {
            XElement commands = configFile.Element("Commands");

            foreach (XElement command in commands.Elements())
            {
                APICommand commandToLoad = new APICommand(command);

                APICommandCollection.Add(commandToLoad.ID, commandToLoad);

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
        #endregion

        /*
        #region API Methods

        #region GetOrderBook

        public abstract OrderBook GetOrderBook();

        public abstract OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency);

        #endregion

        #region GetTick

        /// <summary>
        /// Return the current BTC/USD Tick
        /// </summary>
        /// <returns></returns>
        public abstract Tick GetTick();

        public abstract Tick GetTick(Currency baseCurrency, Currency counterCurrency);

        #endregion

        #region GetTransactions

        /// <summary>
        /// Return transactions from the previous hour
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="counterCurrency"></param>
        /// <returns></returns>
        public abstract List<Transaction> GetTransactions(Currency baseCurrency, Currency counterCurrency);

        /// <summary>
        /// Return BTC/USD transactions from the previous hour
        /// </summary>
        /// <returns></returns>
        public abstract List<Transaction> GetTransactions();

        #endregion

        #endregion
        */

        public Tick GetTick()
        {
            return GetTick(Currency.BTC, Currency.USD);
        }

        public Tick GetTick(Currency baseCurrency, Currency counterCurrency)
        {
            Tick res = null;

            APICommand toExecute = APICommandCollection["Tick"];

            toExecute.BaseCurrency = baseCurrency;
            toExecute.CounterCurrency = counterCurrency;

            string result = ExecuteCommand(toExecute);

            res = DeserializeTick(result, baseCurrency, counterCurrency);

            return res;

        }

        internal Tick DeserializeTick(string tick, Currency baseCurrency, Currency counterCurrency)
        {
            Tick res = null;

            object deserialized = JsonConvert.DeserializeObject<K>(tick);

            MethodInfo conversionMethod = deserialized.GetType().GetMethod("ToTick");

            if (conversionMethod != null)
            {
                res = (Tick)conversionMethod.Invoke(deserialized, new object[] { baseCurrency, counterCurrency });
            }

            return res;
        }


        internal List<Transaction> DeserializeTransactions(string transactions, Currency baseCurrency, Currency counterCurrency)
        {
            List<Transaction> res = null;

            object deserialized = JsonConvert.DeserializeObject<List<A>>(transactions);

            MethodInfo conversionMethod = typeof(A).GetMethod("ToTransactionList");

            if (conversionMethod != null)
            {
                res = (List<Transaction>)conversionMethod.Invoke(null, new object[] { deserialized, baseCurrency, counterCurrency });
            }

            return res;
        }


        public List<Transaction> GetTransactions()
        {

            return GetTransactions(Currency.BTC, Currency.USD);
        }

        /// <summary>
        /// Return transactions that have occurred since the provided DateTime
        /// </summary>
        /// <param name="sinceThisDate"></param>
        /// <returns></returns>
        public List<Transaction> GetTransactions(Currency baseCurrency, Currency counterCurrency)
        {
            List<Transaction> res = new List<Transaction>();

            APICommand toExecute = APICommandCollection["Transactions"];

            SetParameters(toExecute);

            toExecute.BaseCurrency = baseCurrency;
            toExecute.CounterCurrency = counterCurrency;

            //List<BitFinexTransactionJSON> r = ExecuteCommand<List<BitFinexTransactionJSON>>(APICommandCollection["Transactions"]);

            string result = ExecuteCommand(toExecute);

            res = DeserializeTransactions(result, baseCurrency, counterCurrency);
            // return BitFinexTransactionJSON.ConvertBitFinexTransactionList(r, (Currency)toExecute.BaseCurrency, (Currency)toExecute.CounterCurrency);
            return res;

        }


        internal abstract void SetParameters(APICommand command);
    }
}
