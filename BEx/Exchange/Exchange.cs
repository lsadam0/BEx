using BEx.Request;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BEx
{
    internal delegate string GetSignature();

    public abstract class Exchange
    {
        public CurrencyTradingPair DefaultPair
        {
            get;
            set;
        }

        public HashSet<Currency> SupportedCurrencies;

        public HashSet<CurrencyTradingPair> SupportedTradingPairs;

        protected internal ExecutionEngine CommandExecutionEngine;

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

        protected internal Dictionary<CommandClass, ExchangeCommand> CommandCollection;

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
            bool res;

            res = (bool)ExecuteCancelOrderCommand(CommandCollection[CommandClass.CancelOrder], id);
            //res = (bool)SendCommandToDispatcher<B>(CommandCollection["CancelOrder"], defaultPair);

            return res;
        }

        public Order CreateBuyOrder(decimal amount, decimal price)
        {
            return CreateBuyOrder(DefaultPair, amount, price);
        }

        public Order CreateBuyOrder(CurrencyTradingPair pair, decimal amount, decimal price)
        {
            Order res = null;

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
            Order res;

            res = ExecuteOrderCommand(CommandCollection[CommandClass.SellOrder], pair, amount, price);

            return res;
        }

        /// <summary>
        /// Get complete Balance information for your Exchange account
        /// </summary>
        /// <returns>AccountBalance</returns>
        public AccountBalance GetAccountBalance()
        {
            AccountBalance res;

            res = ExecuteAccountBalanceCommand(CommandCollection[CommandClass.AccountBalance], DefaultPair);

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

            res = ExecuteGetDepositAddressCommand(CommandCollection[CommandClass.DepositAddress], toDeposit);

            res.DepositCurrency = toDeposit;

            return res;
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
            OrderBook res = null;

            res = ExecuteOrderBookCommand(CommandCollection[CommandClass.OrderBook], pair);
            //res = (OrderBook)SendCommandToDispatcher<J>(CommandCollection["OrderBook"], baseCurrency, counterCurrency);

            return res;
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
            Tick res = null;

            ExchangeCommand tickCommandReference = CommandCollection[CommandClass.Tick];

            res = ExecuteTickCommand(CommandCollection[CommandClass.Tick], pair);

            return res;
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
            return ExecuteTransactionsCommand(CommandCollection[CommandClass.Transactions], pair);
        }

        public UserTransactions GetUserTransactions()
        {
            return GetUserTransactions(DefaultPair);
        }

        public UserTransactions GetUserTransactions(CurrencyTradingPair pair)
        {
            UserTransactions res;

            res = ExecuteGetUserTransactionsCommand(CommandCollection[CommandClass.UserTransactions], pair);

            return res;
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

        protected abstract AccountBalance ExecuteAccountBalanceCommand(ExchangeCommand command, CurrencyTradingPair pair);

        protected abstract bool ExecuteCancelOrderCommand(ExchangeCommand command, int id);

        protected abstract DepositAddress ExecuteGetDepositAddressCommand(ExchangeCommand command, Currency toDeposit);

        protected abstract OpenOrders ExecuteGetOpenOrdersCommand(ExchangeCommand command, CurrencyTradingPair pair);

        protected abstract UserTransactions ExecuteGetUserTransactionsCommand(ExchangeCommand command, CurrencyTradingPair pair);

        protected abstract OrderBook ExecuteOrderBookCommand(ExchangeCommand command, CurrencyTradingPair pair);

        protected abstract Order ExecuteOrderCommand(ExchangeCommand command, CurrencyTradingPair pair, decimal amount, decimal price);

        protected abstract Tick ExecuteTickCommand(ExchangeCommand command, CurrencyTradingPair pair);

        protected decimal ExecuteTradingFeeCommand(ExchangeCommand command, CurrencyTradingPair pair)
        {
            return 0;
        }

        protected abstract Transactions ExecuteTransactionsCommand(ExchangeCommand command, CurrencyTradingPair pair);

        protected OpenOrders GetOpenOrders(CurrencyTradingPair pair)
        {
            OpenOrders res;

            res = ExecuteGetOpenOrdersCommand(CommandCollection[CommandClass.OrderBook], pair);

            return res;
        }

        #endregion Commands

        protected abstract HashSet<CurrencyTradingPair> GetSupportedTradingPairs();

        protected abstract Dictionary<CommandClass, ExchangeCommand> GetCommandCollection();

        private void BuildConfiguration()
        {
            SupportedTradingPairs = GetSupportedTradingPairs();

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