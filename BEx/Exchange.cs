using System;
using System.Collections.Generic;
using BEx.ExchangeEngine;
using RestSharp;
using BEx.ExchangeEngine.Utilities;

namespace BEx
{
    public abstract class Exchange : IUnauthenticatedExchange, IAuthenticatedExchange
    {
        protected Exchange(IExchangeConfiguration configuration, IExchangeCommandFactory commands, ExchangeType sourceType)
        {
            ExchangeSourceType = sourceType;
            Configuration = configuration;
            Commands = commands;

            CommandExecutionEngine = new ExecutionEngine(this);
        }

        public CurrencyTradingPair DefaultPair
        {
            get
            {
                return Configuration.DefaultPair;
            }
        }

        public ExchangeType ExchangeSourceType
        {
            get;
            protected set;
        }

        public HashSet<Currency> SupportedCurrencies
        {
            get
            {
                return Configuration.SupportedCurrencies;
            }
        }

        public IList<CurrencyTradingPair> SupportedTradingPairs
        {
            get
            {
                return Configuration.SupportedPairs;
            }
        }

        protected internal IAuthenticator Authenticator
        {
            get;
            set;
        }

        protected internal IExchangeCommandFactory Commands
        {
            get;
            private set;
        }

        protected internal IExchangeConfiguration Configuration
        {
            get;
            private set;
        }

        private ExecutionEngine CommandExecutionEngine
        {
            get;
            set;
        }

        public Confirmation CancelOrder(Order toCancel)
        {
            if (toCancel == null)
                throw new ArgumentNullException("toCancel");

            return CancelOrder(toCancel.Id);
        }

        public Confirmation CancelOrder(int id)
        {
            Dictionary<StandardParameterType, string> param = new Dictionary<StandardParameterType, string>()
            {
                { StandardParameterType.Id, id.ToStringInvariant() }
            };

            return (Confirmation)ExecuteCommand(CommandClass.CancelOrder, DefaultPair, param);
        }

        public Order CreateBuyOrder(decimal amount, decimal price)
        {
            return CreateBuyOrder(DefaultPair, amount, price);
        }

        public Order CreateBuyOrder(CurrencyTradingPair pair, decimal amount, decimal price)
        {
            Dictionary<StandardParameterType, string> param = new Dictionary<StandardParameterType, string>()
            {
                { StandardParameterType.Amount, amount.ToStringInvariant() },
                { StandardParameterType.Price, price.ToStringInvariant() }
            };

            return (Order)ExecuteCommand(CommandClass.BuyOrder, pair, param);
        }

        public Order CreateSellOrder(decimal amount, decimal price)
        {
            return CreateSellOrder(DefaultPair, amount, price);
        }

        public Order CreateSellOrder(CurrencyTradingPair pair, decimal amount, decimal price)
        {
            Dictionary<StandardParameterType, string> param = new Dictionary<StandardParameterType, string>()
            {
                { StandardParameterType.Amount, amount.ToStringInvariant() },
                { StandardParameterType.Price, price.ToStringInvariant() }
            };

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
        /// <param name="pair"></param>
        /// <returns></returns>
        public OrderBook GetOrderBook(CurrencyTradingPair pair)
        {
            return (OrderBook)ExecuteCommand(CommandClass.OrderBook, pair);
        }


        public Tick GetTick()
        {
            return GetTick(DefaultPair);
        }


        public Tick GetTick(CurrencyTradingPair pair)
        {
            return (Tick)ExecuteCommand(CommandClass.Tick, pair);
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
        /// <param name="pair"></param>
        /// <returns></returns>
        public Transactions GetTransactions(CurrencyTradingPair pair)
        {
            Dictionary<StandardParameterType, string> values = new Dictionary<StandardParameterType, string>()
            {
                {
                    StandardParameterType.UnixTimestamp,
                    UnixTime.DateTimeToUnixTimestamp(DateTime.UtcNow.AddHours(-1)).ToStringInvariant()
                }
            };

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
        /// <param name="pair">Currency Pair</param>
        /// <returns>True if supported, otherwise false.</returns>
        public bool IsTradingPairSupported(CurrencyTradingPair pair)
        {
            return Configuration.SupportedPairs.Contains(pair);
        }

        public OpenOrders GetOpenOrders(CurrencyTradingPair pair)
        {
            return (OpenOrders)ExecuteCommand(CommandClass.OpenOrders, pair);
        }

        private ApiResult ExecuteCommand(CommandClass commandType, CurrencyTradingPair pair, Dictionary<StandardParameterType, string> values = null)
        {
            ExchangeCommand command = Commands.GetCommand(commandType);

            if (command.HasDependentParameters)
                return CommandExecutionEngine.ExecuteCommand(command, pair, values);
            else
                return CommandExecutionEngine.ExecuteCommand(command, pair);
        }
    }
}