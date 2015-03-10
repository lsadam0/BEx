using BEx.Request;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BEx
{
    internal delegate string GetSignature();

    public abstract class Exchange
    {
        public HashSet<Currency> SupportedCurrencies
        {
            get;
            private set;
        }

        public HashSet<CurrencyTradingPair> SupportedTradingPairs
        {
            get;
            private set;
        }

        internal ExecutionEngine CommandExecutionEngine;

        private Dictionary<CommandClass, ExchangeCommand> CommandCollection;

        protected Exchange(ExchangeType exchangeSourceType, string baseUrl, IExchangeCommandFactory commandFactory)
        {
            DefaultPair = new CurrencyTradingPair(Currency.BTC, Currency.USD);

            CommandCollection = commandFactory.GetCommandCollection();

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
            private set;
        }

        public ExchangeType ExchangeSourceType
        {
            get;
            private set;
        }

        private long _nonce = DateTime.Now.Ticks;

        /// <summary>
        /// Consecutively increasing action counter
        /// </summary>
        /// <value>0</value>
        public long Nonce
        {
            get
            {
                return Interlocked.Increment(ref _nonce);
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

        private APIResult ExecuteCommand(CommandClass commandType, CurrencyTradingPair pair, Dictionary<StandardParameterType, string> values = null)
        {
            ExchangeCommand command = CommandCollection[commandType];

            if (command.HasDependentParameters)
            {
                return CommandExecutionEngine.ExecuteCommand(command, pair, values);
            }
            else
                return CommandExecutionEngine.ExecuteCommand(command, pair);
        }

        public Confirmation CancelOrder(Order toCancel)
        {
            return CancelOrder(toCancel.ID);
        }

        public Confirmation CancelOrder(int id)
        {
            Dictionary<StandardParameterType, string> param = new Dictionary<StandardParameterType, string>();
            param.Add(StandardParameterType.Id, id.ToString());

            return (Confirmation)ExecuteCommand(CommandClass.CancelOrder, DefaultPair, param);
        }

        public Order CreateBuyOrder(decimal amount, decimal price)
        {
            return CreateBuyOrder(DefaultPair, amount, price);
        }

        public Order CreateBuyOrder(CurrencyTradingPair pair, decimal amount, decimal price)
        {
            Dictionary<StandardParameterType, string> param = new Dictionary<StandardParameterType, string>();

            param.Add(StandardParameterType.Amount, amount.ToString());
            param.Add(StandardParameterType.Price, price.ToString());

            return (Order)ExecuteCommand(CommandClass.BuyOrder, pair, param);
        }

        public Order CreateSellOrder(decimal amount, decimal price)
        {
            return CreateSellOrder(DefaultPair, amount, price);
        }

        public Order CreateSellOrder(CurrencyTradingPair pair, decimal amount, decimal price)
        {
            Dictionary<StandardParameterType, string> param = new Dictionary<StandardParameterType, string>();

            param.Add(StandardParameterType.Amount, amount.ToString());
            param.Add(StandardParameterType.Price, price.ToString());

            return (Order)ExecuteCommand(CommandClass.SellOrder, pair, param);
        }

        /// <summary>
        /// Get complete Balance information for your Exchange account
        /// </summary>
        /// <returns>AccountBalance</returns>
        public AccountBalance GetAccountBalance()
        {
            return (AccountBalance)ExecuteCommand(CommandClass.AccountBalance, DefaultPair);
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
            CurrencyTradingPair pair = new CurrencyTradingPair(toDeposit, toDeposit);

            return (DepositAddress)ExecuteCommand(CommandClass.DepositAddress, pair);
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
            return (OrderBook)ExecuteCommand(CommandClass.OrderBook, pair);
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
            return (Tick)ExecuteCommand(CommandClass.Tick, pair);
            // return (Tick)CommandExecutionEngine.ExecuteCommand(CommandCollection[CommandClass.Tick], pair);
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
            Dictionary<StandardParameterType, string> values = new Dictionary<StandardParameterType, string>();

            values.Add(StandardParameterType.UnixTimeStamp, Common.UnixTime.DateTimeToUnixTimestamp(DateTime.UtcNow.AddHours(-1)).ToString());

            return (Transactions)ExecuteCommand(CommandClass.Transactions, pair, values);
        }

        public UserTransactions GetUserTransactions()
        {
            return GetUserTransactions(DefaultPair);
        }

        public UserTransactions GetUserTransactions(CurrencyTradingPair pair)
        {
            return (UserTransactions)ExecuteCommand(CommandClass.UserTransactions, pair);
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
            return (OpenOrders)ExecuteCommand(CommandClass.OpenOrders, pair);
        }

        #endregion Commands

        protected abstract HashSet<CurrencyTradingPair> GetSupportedTradingPairs();

        protected internal abstract bool IsError(string content);

        protected internal abstract APIError DetermineErrorCondition(string message);

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
        }
    }
}