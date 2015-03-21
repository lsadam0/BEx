// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using BEx.ExchangeEngine;
using RestSharp;
using BEx.ExchangeEngine.Utilities;

namespace BEx
{
    public abstract class Exchange : IUnauthenticatedExchange, IAuthenticatedExchange
    {
        internal Exchange(IExchangeConfiguration configuration, IExchangeCommandFactory commands, ExchangeType sourceType)
        {
            ExchangeSourceType = sourceType;
            Configuration = configuration;
            Commands = commands;

            Commands.BuildCommands(new ExecutionEngine(this));
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

        internal IExchangeCommandFactory Commands
        {
            get;
            private set;
        }

        protected internal IExchangeConfiguration Configuration
        {
            get;
            private set;
        }



        public Confirmation CancelOrder(Order toCancel)
        {
            if (toCancel == null)
                throw new ArgumentNullException("toCancel");

            return CancelOrder(toCancel.Id);
        }

        public Confirmation CancelOrder(int id)
        {
            Dictionary<StandardParameter, string> values = new Dictionary<StandardParameter, string>()
            {
                { StandardParameter.Id, id.ToStringInvariant() }
            };

            return Commands.CancelOrder.Execute(values) as Confirmation;
        }

        public Order CreateBuyOrder(decimal amount, decimal price)
        {
            return CreateBuyOrder(DefaultPair, amount, price);
        }

        public Order CreateBuyOrder(CurrencyTradingPair pair, decimal amount, decimal price)
        {
            Dictionary<StandardParameter, string> values = new Dictionary<StandardParameter, string>()
            {
                {StandardParameter.Amount, amount.ToStringInvariant()},
                {StandardParameter.Price, price.ToStringInvariant()}
            };

            return Commands.BuyOrder.Execute(pair, values) as Order;
        }

        public Order CreateSellOrder(decimal amount, decimal price)
        {
            return CreateSellOrder(DefaultPair, amount, price);
        }

        public Order CreateSellOrder(CurrencyTradingPair pair, decimal amount, decimal price)
        {
            Dictionary<StandardParameter, string> values = new Dictionary<StandardParameter, string>()
            {
                { StandardParameter.Amount, amount.ToStringInvariant() },
                { StandardParameter.Price, price.ToStringInvariant() }
            };

            return Commands.SellOrder.Execute(pair, values) as Order;
        }

        /// <summary>
        /// Url complete Balance information for your Exchange account
        /// </summary>
        /// <returns>AccountBalance</returns>
        public AccountBalance GetAccountBalance()
        {
            return Commands.AccountBalance.Execute() as AccountBalance;
        }

        /// <summary>
        /// Url your BTC Deposit Url for the Exchange
        /// </summary>
        /// <returns>DepositAddress</returns>
        public DepositAddress GetDepositAddress()
        {
            return GetDepositAddress(Currency.BTC);
        }

        /// <summary>
        /// Url the Deposit Url for the requested CryptoCurrency
        /// </summary>
        /// <param name="toDeposit">CryptoCurrency to deposit</param>
        /// <returns></returns>
        public DepositAddress GetDepositAddress(Currency toDeposit)
        {
            CurrencyTradingPair pair = new CurrencyTradingPair(toDeposit, toDeposit);

            return Commands.DepositAddress.Execute(pair) as DepositAddress;
        }

        public OpenOrders GetOpenOrders()
        {
            return GetOpenOrders(DefaultPair);
        }

        /// <summary>
        /// Url the current BTC/USD Order Book.
        /// </summary>
        /// <returns></returns>
        public OrderBook GetOrderBook()
        {
            return GetOrderBook(DefaultPair);
        }

        /// <summary>
        /// Url the current Order Book for the specified Currency pair.
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        public OrderBook GetOrderBook(CurrencyTradingPair pair)
        {
            return Commands.OrderBook.Execute(pair) as OrderBook;
        }


        public Tick GetTick()
        {
            return GetTick(DefaultPair);
        }


        public Tick GetTick(CurrencyTradingPair pair)
        {
            return Commands.Tick.Execute(pair) as Tick;
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
            Dictionary<StandardParameter, string> values = new Dictionary<StandardParameter, string>()
            {
                {
                    StandardParameter.UnixTimestamp,
                    UnixTime.DateTimeToUnixTimestamp(DateTime.UtcNow.AddHours(-1)).ToStringInvariant()
                }
            };

            return Commands.Transactions.Execute(pair, values) as Transactions;
        }

        public UserTransactions GetUserTransactions()
        {
            return GetUserTransactions(DefaultPair);
        }

        public UserTransactions GetUserTransactions(CurrencyTradingPair pair)
        {
            return Commands.UserTransactions.Execute(pair) as UserTransactions;

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
            return Commands.OpenOrders.Execute(pair) as OpenOrders;
        }

    }
}