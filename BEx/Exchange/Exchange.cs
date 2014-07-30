using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Security.Cryptography;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using RestSharp;
using BEx.BitFinexSupport;

namespace BEx
{

    public delegate string GetSignature();

    public abstract class Exchange : IDisposable
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

        protected RestClient authenticatedClient
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

        public Uri AuthenticatedURI
        {
            get;
            set;
        }

        public string APIKey
        {
            get;
            set;
        }

        public string SecretKey
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

        public string ClientID
        {
            get;
            set;
        }

        protected Dictionary<string, APICommand> APICommandCollection
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
        #endregion

        protected Exchange(string configFile)
        {
            LoadConfigFromXML(configFile);

            apiClient = new RestClient(BaseURI.ToString());

            if (AuthenticatedURI != null)
                authenticatedClient = new RestClient(AuthenticatedURI.ToString());
            else
                authenticatedClient = null;

            apiRequestFactory = new RequestFactory();
            apiRequestFactory.GetSignature += CreateSignature;
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

        #endregion

        #region API Commands

        #region GetOrderBook

        public abstract OrderBook GetOrderBook();

        public abstract OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency);


        protected OrderBook GetOrderBook<J>(Currency baseCurrency, Currency counterCurrency)
        {
            OrderBook res = null;

            res = (OrderBook)SendCommandToDispatcher<J>(APICommandCollection["OrderBook"], baseCurrency, counterCurrency);

            return res;
        }


        #endregion

        #region GetTick

        public abstract Tick GetTick();

        public abstract Tick GetTick(Currency baseCurrency, Currency counterCurrency);

        protected Tick GetTick<J>(Currency baseCurrency, Currency counterCurrency)
        {
            Tick res = null;

            res = (Tick)SendCommandToDispatcher<J>(APICommandCollection["Tick"], baseCurrency, counterCurrency);

            return res;
        }


        #endregion

        #region GetTransactions

        public abstract Transactions GetTransactions();

        /// <summary>
        /// Return transactions that have occurred since the provided DateTime
        /// </summary>
        /// <param name="sinceThisDate"></param>
        /// <returns></returns>
        public abstract Transactions GetTransactions(Currency baseCurrency, Currency counterCurrency);

        protected Transactions GetTransactions<J>(Currency baseCurrency, Currency counterCurrency)
        {
            Transactions res = new Transactions();

            res = (Transactions)SendCommandToDispatcher<List<J>>(APICommandCollection["Transactions"], baseCurrency, counterCurrency);

            return res;
        }

        #endregion

        #region Account Balance

        public abstract AccountBalance GetAccountBalance();

        protected AccountBalance GetAccountBalance<B>(Currency baseCurrency, Currency counterCurrency)
        {
            AccountBalance res;

            res = (AccountBalance)SendCommandToDispatcher<B>(APICommandCollection["AccountBalance"], Currency.BTC, Currency.USD);

            return res;
        }

        #endregion

        #region Buy Limit Order

        public abstract Order CreateBuyOrder(decimal amount, decimal price);

        public abstract Order CreateBuyOrder(Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price);

        protected Order CreateBuyOrder<B>(Currency baseCurrency, Currency counterCurrency)
        {
            Order res;

            res = (Order)SendCommandToDispatcher<B>(APICommandCollection["BuyOrder"], Currency.BTC, Currency.USD);

            return res;
        }

        #endregion

        #region Sell Limit Order

        public abstract Order CreateSellOrder(decimal amount, decimal price);

        public abstract Order CreateSellOrder(Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price);

        protected Order CreateSellOrder<B>(Currency baseCurrency, Currency counterCurrency)
        {
            Order res;

            res = (Order)SendCommandToDispatcher<B>(APICommandCollection["SellOrder"], Currency.BTC, Currency.USD);

            return res;
        }

        #endregion

        #region Open Orders

        public abstract OpenOrders GetOpenOrders();

        protected OpenOrders GetOpenOrders<B>(Currency baseCurrency, Currency counterCurrency)
        {
            OpenOrders res;

            res = (OpenOrders)SendCommandToDispatcher<List<B>>(APICommandCollection["OpenOrders"], baseCurrency, counterCurrency);

            return res;

        }
        #endregion

        #region User Transactions

        public abstract UserTransactions GetUserTransactions();

        protected UserTransactions GetUserTransactions<B>(Currency baseCurrency, Currency counterCurrency)
        {
            UserTransactions res;

            res = (UserTransactions)SendCommandToDispatcher<List<B>>(APICommandCollection["UserTransactions"], baseCurrency, counterCurrency);

            return res;
        }

        #endregion

        #region Cancel Order

        public abstract bool CancelOrder(int id);

        public abstract bool CancelOrder(Order toCancel);

        protected bool CancelOrder<B>(int id)
        {
            bool res;

            res = (bool)SendCommandToDispatcher<B>(APICommandCollection["CancelOrder"], Currency.BTC, Currency.USD);

            return res;
        }

        #endregion

        #region Deposit Address

        public abstract string GetDepositAddress();

        public abstract string GetDepositAddress(Currency toDeposit);
        

        protected object GetDepositAddress<B>(Currency toDeposit)
        {
            object res;

            res = SendCommandToDispatcher<B>(APICommandCollection["DepositAddress"], toDeposit);

            return res;
        }

        #endregion

        #region Withdraw

        public abstract string Withdraw();

        public abstract string Withdraw(Currency toWithdraw);

        protected object Withdraw<B>(Currency toWithdraw)
        {
            object res;

            res = SendCommandToDispatcher<string>(APICommandCollection["Withdraw"], toWithdraw);

            return res;
        }

        #endregion

        public abstract object PendingDeposits();

        protected object PendingDeposits<B>()
        {
            object res = null;

            //res = SendCommandToDispatcher<object>(APICommandCollection["PendingDeposits"])

            return res;
        }

        public abstract object PendingWithdrawals();

        protected object PendingWithdrawals<B>()
        {
            object res = null;

            return res;
        }
        #endregion

        private object SendCommandToDispatcher<J>(APICommand toExecute, Currency baseCurrency, Currency? counterCurrency = null)
        {
            toExecute.BaseCurrency = baseCurrency;
            toExecute.CounterCurrency = counterCurrency;

            VerifyCurrencySupport(toExecute);

            RequestDispatcher<J> disp = null;
            if (toExecute.RequiresAuthentication && authenticatedClient != null)
                disp = new RequestDispatcher<J>(authenticatedClient, apiRequestFactory);
            else
                disp = new RequestDispatcher<J>(apiClient, apiRequestFactory);

            return disp.ExecuteCommand(toExecute);
        }

        protected abstract void CreateSignature(RestRequest request, APICommand command);


        #region Dispose
        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

                //
            }
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

            if (configFile.Element("AuthenticatedURL") != null)
                AuthenticatedURI = new Uri(configFile.Element("AuthenticatedURL").Value);

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
    }
}
