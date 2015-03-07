using BEx.Request;
using RestSharp;
using System;
using System.Collections.Generic;

namespace BEx
{
    internal delegate string GetSignature();

    public abstract class Exchange
    {
        public HashSet<Currency> SupportedCurrencies;

        public HashSet<CurrencyTradingPair> SupportedTradingPairs;

        internal ExecutionEngine CommandExecutionEngine;

        protected internal Dictionary<CommandClass, ExchangeCommand> CommandCollection;

        protected Exchange(ExchangeType exchangeSourceType, string baseUrl)
        {
            DefaultPair = new CurrencyTradingPair(Currency.BTC, Currency.USD);

            ExchangeSourceType = exchangeSourceType;

            BaseURI = new Uri(baseUrl.TrimEnd('/', '\\'));

            BuildConfiguration();

            CommandExecutionEngine = new ExecutionEngine(this);
        }

        public Uri BaseURI
        {
            get;
            set;
        }

        public CurrencyTradingPair DefaultPair
        {
            get;
            set;
        }

        public ExchangeType ExchangeSourceType
        {
            get;
            private set;
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

        #region Commands

        public bool CancelOrder(Order toCancel)
        {
            return CancelOrder(toCancel.ID);
        }

        public bool CancelOrder(int id)
        {
            return false;
            /*
            bool res;

            res = (bool)BuildCancelOrderCommand(CommandCollection[CommandClass.CancelOrder], id);
            //res = (bool)SendCommandToDispatcher<B>(CommandCollection["CancelOrder"], defaultPair);

            return res;*/
        }

        public Order CreateBuyOrder(decimal amount, decimal price)
        {
            return CreateBuyOrder(DefaultPair, amount, price);
        }

        public Order CreateBuyOrder(CurrencyTradingPair pair, decimal amount, decimal price)
        {
            Order res = null;

            ExchangeCommand toExecute = CommandCollection[CommandClass.BuyOrder];

            Dictionary<StandardParameterType, string> setValues = new Dictionary<StandardParameterType, string>();

            setValues.Add(StandardParameterType.Amount, amount.ToString());
            setValues.Add(StandardParameterType.Price, price.ToString());

            this.CommandExecutionEngine.ExecuteCommand(toExecute, pair, setValues);

            // res = ExecuteOrderCommand(CommandCollection["BuyOrder"], baseCurrency, counterCurrency, amount, price);
            //res = (Order)SendCommandToDispatcher<B>(CommandCollection["BuyOrder"], defaultPair);

            return res;
        }

        public Order CreateSellOrder(decimal amount, decimal price)
        {
            return CreateSellOrder(DefaultPair, amount, price);
        }

        public Order CreateSellOrder(CurrencyTradingPair pair, decimal amount, decimal price)
        {
            return null;
            /*
            Order res;

            res = BuildOrderCommand(CommandCollection[CommandClass.SellOrder], pair, amount, price);

            return res;*/
        }

        /// <summary>
        /// Get complete Balance information for your Exchange account
        /// </summary>
        /// <returns>AccountBalance</returns>
        public AccountBalance GetAccountBalance()
        {
            return null;
            /*
            AccountBalance res;

            res = BuildAccountBalanceCommand(CommandCollection[CommandClass.AccountBalance], DefaultPair);

            return res;*/
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
            return null;
            /*
            DepositAddress res;

            res = BuildDepositAddressCommand(CommandCollection[CommandClass.DepositAddress], toDeposit);

            res.DepositCurrency = toDeposit;

            return res;*/
        }

        public OpenOrders GetOpenOrders()
        {
            return GetOpenOrders(DefaultPair);
        }

        /// <summary>
        /// Get the current BTC/USD Order Book.
        /// </summary>
        /// <returns></returns>
        public OrderBook GetOrderBook()
        {
            return GetOrderBook(DefaultPair);
        }

        /// <summary>
        /// Get the current Order Book for the specified Currency pair.
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="counterCurrency"></param>
        /// <returns></returns>
        public OrderBook GetOrderBook(CurrencyTradingPair pair)
        {
            return null;
            //ExchangeCommand orderBook = CommandCollection[CommandClass.OrderBook];

            /*
            OrderBook res = null;

            res = BuildOrderBookCommand(CommandCollection[CommandClass.OrderBook], pair);
            //res = (OrderBook)SendCommandToDispatcher<J>(CommandCollection["OrderBook"], baseCurrency, counterCurrency);

            return res;*/
        }

        /// <summary>
        /// Get the current BTC/USD Tick.
        /// </summary>
        /// <returns></returns>
        public Tick GetTick()
        {
            return GetTick(DefaultPair);
        }

        /// <summary>
        /// Get the current Tick for the specified currency pair.
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="counterCurrency"></param>
        /// <returns></returns>
        public Tick GetTick(CurrencyTradingPair pair)
        {
            return (Tick)CommandExecutionEngine.ExecuteCommand(CommandCollection[CommandClass.Tick], pair);
        }

        /// <summary>
        /// Return BTC/USD general Transactions for past hour.
        /// </summary>
        /// <returns></returns>
        public Transactions GetTransactions()
        {
            return GetTransactions(DefaultPair);
        }

        /// <summary>
        /// Return general Transactions from the past hour for the specified currency pair.
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="counterCurrency"></param>
        /// <returns></returns>
        public Transactions GetTransactions(CurrencyTradingPair pair)
        {
            ExchangeCommand cmd = CommandCollection[CommandClass.Transactions];

            Dictionary<StandardParameterType, string> param = null;
            if (cmd.DependentParameters.ContainsKey(StandardParameterType.UnixTimeStamp))
            {
                param = new Dictionary<StandardParameterType, string>();

                param.Add(StandardParameterType.UnixTimeStamp, Common.UnixTime.DateTimeToUnixTimestamp(DateTime.Now.AddHours(-1)).ToString());
            }

            return (Transactions)CommandExecutionEngine.ExecuteCommand(cmd, pair, param);

            // return ExecuteTransactionsCommand(CommandCollection[CommandClass.Transactions], pair);
        }

        public UserTransactions GetUserTransactions()
        {
            return GetUserTransactions(DefaultPair);
        }

        public UserTransactions GetUserTransactions(CurrencyTradingPair pair)
        {
            return null;
            /*
            UserTransactions res;

            res = BuildUserTransactionsCommand(CommandCollection[CommandClass.UserTransactions], pair);

            return res;*/
        }

        /// <summary>
        /// Verify that a currency pair (e.g. BTC/USD) is supported by this exchange.
        /// </summary>
        /// <param name="baseCurrency">Base Currency</param>
        /// <param name="counterCurrency">Counter Currency</param>
        /// <returns>True if supported, otherwise false.</returns>
        public bool IsTradingPairSupported(CurrencyTradingPair pair)
        {
            return SupportedTradingPairs.Contains(pair);
        }

        protected internal abstract void CreateSignature(RestRequest request, ExchangeCommand command, CurrencyTradingPair pair, Dictionary<string, string> parameters = null);

        protected OpenOrders GetOpenOrders(CurrencyTradingPair pair)
        {
            return null;
            /*
            OpenOrders res;

            res = BuildOpenOrdersCommand(CommandCollection[CommandClass.OrderBook], pair);

            return res;*/
        }

        #endregion Commands

        protected abstract Dictionary<CommandClass, ExchangeCommand> GetCommandCollection();

        protected abstract HashSet<CurrencyTradingPair> GetSupportedTradingPairs();

        private void BuildConfiguration()
        {
            SupportedTradingPairs = GetSupportedTradingPairs();
            SupportedCurrencies = new HashSet<Currency>();

            foreach (CurrencyTradingPair pair in SupportedTradingPairs)
            {
                if (!SupportedCurrencies.Contains(pair.BaseCurrency))
                    SupportedCurrencies.Add(pair.BaseCurrency);

                if (!SupportedCurrencies.Contains(pair.CounterCurrency))
                    SupportedCurrencies.Add(pair.CounterCurrency);
            }

            CommandCollection = GetCommandCollection();
        }
    }
}