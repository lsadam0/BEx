using RestSharp;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BEx
{
    internal delegate string GetSignature();

    public abstract class Exchange
    {
        public HashSet<Currency> SupportedCurrencies;

        /// <summary>
        /// Collection of currency pairs supported by the current exchange indexed by the base currency
        /// </summary>
        public Dictionary<Currency, HashSet<Currency>> SupportedTradingPairs;

        protected Exchange(string configFile)
        {
            LoadConfigFromXML(configFile);

            string apiClient = null;

            apiClient = BaseURI.ToString();

            dispatcher = new RequestDispatcher(apiClient);

            apiRequestFactory = new RequestFactory();
            apiRequestFactory.GetSignature += CreateSignature;
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

        internal RequestDispatcher dispatcher
        {
            get;
            set;
        }

        protected internal string APIKey
        {
            get;
            set;
        }

        protected internal string ClientID
        {
            get;
            set;
        }

        protected internal string SecretKey
        {
            get;
            set;
        }

        protected Dictionary<string, APICommand> APICommandCollection
        {
            get;
            set;
        }

        private RequestFactory apiRequestFactory
        {
            get;
            set;
        }

        public bool CancelOrder(Order toCancel)
        {
            return CancelOrder(toCancel.ID);
        }

        public bool CancelOrder(int id)
        {
            bool res;

            res = (bool)ExecuteCancelOrderCommand(APICommandCollection["CancelOrder"], id);
            //res = (bool)SendCommandToDispatcher<B>(APICommandCollection["CancelOrder"], Currency.BTC, Currency.USD);

            return res;
        }

        public Order CreateBuyOrder(decimal amount, decimal price)
        {
            return CreateBuyOrder(Currency.BTC, Currency.USD, amount, price);
        }

        public Order CreateBuyOrder(Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price)
        {
            Order res;

            res = ExecuteOrderCommand(APICommandCollection["BuyOrder"], baseCurrency, counterCurrency, amount, price);
            //res = (Order)SendCommandToDispatcher<B>(APICommandCollection["BuyOrder"], Currency.BTC, Currency.USD);

            return res;
        }

        public Order CreateSellOrder(decimal amount, decimal price)
        {
            return CreateSellOrder(Currency.BTC, Currency.USD, amount, price);
        }

        public Order CreateSellOrder(Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price)
        {
            Order res;

            res = ExecuteOrderCommand(APICommandCollection["SellOrder"], baseCurrency, counterCurrency, amount, price);

            return res;
        }

        /// <summary>
        /// Get complete Balance information for your Exchange account
        /// </summary>
        /// <returns>AccountBalance</returns>
        public AccountBalance GetAccountBalance()
        {
            AccountBalance res;

            res = ExecuteAccountBalanceCommand(APICommandCollection["AccountBalance"], Currency.BTC, Currency.USD);

            return res;
        }

        /// <summary>
        /// Get your BTC Deposit Address for the Exchange
        /// </summary>
        /// <returns>DepositAddress</returns>
        public DepositAddress GetDepositAddress()
        {
            return GetDepositAddress(Currency.BTC);
        }

        /// <summary>
        /// Get the Deposit Address for the requested CryptoCurrency
        /// </summary>
        /// <param name="toDeposit">CryptoCurrency to deposit</param>
        /// <returns></returns>
        public DepositAddress GetDepositAddress(Currency toDeposit)
        {
            DepositAddress res;

            res = ExecuteGetDepositAddressCommand(APICommandCollection["DepositAddress"], toDeposit);

            res.DepositCurrency = toDeposit;

            return res;
        }

        public OpenOrders GetOpenOrders()
        {
            return GetOpenOrders(Currency.BTC, Currency.USD);
        }

        /// <summary>
        /// Get the current BTC/USD Order Book.
        /// </summary>
        /// <returns></returns>
        public OrderBook GetOrderBook()
        {
            return GetOrderBook(Currency.BTC, Currency.USD);
        }

        /// <summary>
        /// Get the current Order Book for the specified Currency pair.
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="counterCurrency"></param>
        /// <returns></returns>
        public OrderBook GetOrderBook(Currency baseCurrency, Currency counterCurrency)
        {
            OrderBook res = null;

            res = ExecuteOrderBookCommand(APICommandCollection["OrderBook"], baseCurrency, counterCurrency);
            //res = (OrderBook)SendCommandToDispatcher<J>(APICommandCollection["OrderBook"], baseCurrency, counterCurrency);

            return res;
        }

        /// <summary>
        /// Get the current BTC/USD Tick.
        /// </summary>
        /// <returns></returns>
        public Tick GetTick()
        {
            return GetTick(Currency.BTC, Currency.USD);
        }

        /// <summary>
        /// Get the current Tick for the specified currency pair.
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="counterCurrency"></param>
        /// <returns></returns>
        public Tick GetTick(Currency baseCurrency, Currency counterCurrency)
        {
            Tick res = null;

            res = ExecuteTickCommand(APICommandCollection["Tick"], baseCurrency, counterCurrency);

            return res;
        }

        /// <summary>
        /// Return BTC/USD general Transactions for past hour.
        /// </summary>
        /// <returns></returns>
        public Transactions GetTransactions()
        {
            return GetTransactions(Currency.BTC, Currency.USD);
        }

        /// <summary>
        /// Return general Transactions from the past hour for the specified currency pair.
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="counterCurrency"></param>
        /// <returns></returns>
        public Transactions GetTransactions(Currency baseCurrency, Currency counterCurrency)
        {
            Transactions res = new Transactions(DateTime.Now);

            res = ExecuteTransactionsCommand(APICommandCollection["Transactions"], baseCurrency, counterCurrency);

            return res;
        }

        public UserTransactions GetUserTransactions()
        {
            return GetUserTransactions(Currency.BTC, Currency.USD);
        }

        public UserTransactions GetUserTransactions(Currency baseCurrency, Currency counterCurrency)
        {
            UserTransactions res;

            res = ExecuteGetUserTransactionsCommand(APICommandCollection["UserTransactions"], baseCurrency, counterCurrency);

            return res;
        }

        /// <summary>
        /// Verify that a currency pair (e.g. BTC/USD) is supported by this exchange.
        /// </summary>
        /// <param name="baseCurrency">Base Currency</param>
        /// <param name="counterCurrency">Counter Currency</param>
        /// <returns>True if supported, otherwise false.</returns>
        public bool IsCurrencyPairSupported(Currency baseCurrency, Currency counterCurrency)
        {
            bool res = false;

            if (SupportedTradingPairs.ContainsKey(baseCurrency))
            {
                if (SupportedTradingPairs[baseCurrency].Contains(counterCurrency))
                {
                    res = true;
                }
            }

            return res;
        }

        protected internal abstract void CreateSignature(RestRequest request, APICommand command, Currency baseCurrency, Currency counterCurrency, Dictionary<string, string> parameters = null);

        protected internal object SendCommandToDispatcher<J, E>(APICommand toExecute, Currency baseCurrency, Currency counterCurrency, Dictionary<string, string> parameters = null)
        {
            RestRequest request = apiRequestFactory.GetRequest(toExecute, baseCurrency, counterCurrency, parameters);

            return dispatcher.ExecuteCommand<J, E>(request, toExecute, baseCurrency, counterCurrency);
        }

        protected abstract AccountBalance ExecuteAccountBalanceCommand(APICommand command, Currency baseCurrency, Currency counterCurrency);

        protected abstract bool ExecuteCancelOrderCommand(APICommand command, int id);

        protected abstract DepositAddress ExecuteGetDepositAddressCommand(APICommand command, Currency toDeposit);

        protected abstract OpenOrders ExecuteGetOpenOrdersCommand(APICommand command, Currency baseCurrency, Currency counterCurrency);

        protected abstract UserTransactions ExecuteGetUserTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency);

        protected abstract OrderBook ExecuteOrderBookCommand(APICommand command, Currency baseCurrency, Currency counterCurrency);

        protected abstract Order ExecuteOrderCommand(APICommand command, Currency baseCurrency, Currency counterCurrency, decimal amount, decimal price);

        protected abstract Tick ExecuteTickCommand(APICommand command, Currency baseCurrency, Currency counterCurrency);

        protected decimal ExecuteTradingFeeCommand(APICommand command, Currency baseCurrency, Currency counterCurrency)
        {
            return 0;
        }

        protected abstract Transactions ExecuteTransactionsCommand(APICommand command, Currency baseCurrency, Currency counterCurrency);

        protected OpenOrders GetOpenOrders(Currency baseCurrency, Currency counterCurrency)
        {
            OpenOrders res;

            res = ExecuteGetOpenOrdersCommand(APICommandCollection["OpenOrders"], baseCurrency, counterCurrency);

            return res;
        }

        private decimal GetTradingFee(Currency feeCurrency)
        {
            return ExecuteTradingFeeCommand(APICommandCollection["TradingFee"], feeCurrency, Currency.USD);
        }

        private void LoadCommands(XElement configFile)
        {
            XElement commands = configFile.Element("Commands");

            foreach (XElement command in commands.Elements())
            {
                APICommand commandToLoad = new APICommand(command);

                APICommandCollection.Add(commandToLoad.ID, commandToLoad);
            }
        }

        private void LoadConfigFromXML(string file)
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

            SupportedTradingPairs = new Dictionary<Currency, HashSet<Currency>>();
            SupportedCurrencies = new HashSet<Currency>();

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
                    if (!SupportedTradingPairs.ContainsKey(bs))
                    {
                        SupportedTradingPairs.Add(bs, new HashSet<Currency>());
                    }

                    if (!SupportedCurrencies.Contains(bs))
                        SupportedCurrencies.Add(bs);

                    if (!SupportedCurrencies.Contains(cs))
                        SupportedCurrencies.Add(cs);

                    if (!SupportedTradingPairs[bs].Contains(cs))
                    {
                        SupportedTradingPairs[bs].Add(cs);
                    }
                }
            }
        }
    }
}