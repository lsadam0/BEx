﻿// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BEx.Exceptions;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Utilities;

namespace BEx
{
    public abstract class Exchange : IExchange
    {
        private readonly ExecutionEngine executor;

        internal Exchange(
            IExchangeConfiguration configuration,
            IExchangeCommandFactory commands)
        {
            Configuration = configuration;
            Commands = commands;

            executor = new ExecutionEngine(
                configuration.BaseUri,
                configuration.ExchangeSourceType);
        }

        internal Exchange(
            IExchangeConfiguration configuration,
            IExchangeCommandFactory commands,
            IExchangeAuthenticator authenticator)
        {
            Configuration = configuration;
            Commands = commands;
            Authenticator = authenticator;

            executor = new ExecutionEngine(
                configuration.BaseUri,
                authenticator,
                configuration.ExchangeSourceType);
        }

        internal IExchangeAuthenticator Authenticator { get; private set; }

        internal IExchangeCommandFactory Commands { get; set; }

        protected internal IExchangeConfiguration Configuration { get; }

        public TradingPair DefaultPair
        {
            get { return Configuration.DefaultPair; }
        }

        public ExchangeType ExchangeSourceType
        {
            get { return Configuration.ExchangeSourceType; }
        }

        public ImmutableHashSet<Currency> SupportedCurrencies
        {
            get { return Configuration.SupportedCurrencies; }
        }

        public ImmutableHashSet<TradingPair> SupportedTradingPairs
        {
            get { return Configuration.SupportedPairs; }
        }

        public Confirmation CancelOrder(Order toCancel)
        {
            if (toCancel == null)
                throw new ArgumentNullException("toCancel");

            return CancelOrder(toCancel.Id);
        }

        public Confirmation CancelOrder(int id)
        {
            var values = new Dictionary<StandardParameter, string>
            {
                {StandardParameter.Id, id.ToStringInvariant()}
            };

            return executor.Execute(
                Commands.CancelOrder,
                values);
        }

        /// <summary>
        ///     Create a Limit Order to buy "amount" of base currency at the counter currency "price" using the default trading
        ///     pair.
        /// </summary>
        /// <param name="amount">Amount of Base Currency to Buy</param>
        /// <param name="price">Counter Currency Purchase Price</param>
        /// <returns>BEx.Order</returns>
      
        public Order CreateBuyLimitOrder(decimal amount, decimal price)
        {
            return CreateBuyLimitOrder(DefaultPair, amount, price);
        }

        /// <summary>
        ///     Create a Limit Order to buy "amount" of base currency at the counter currency "price"
        /// </summary>
        /// <param name="pair">Trading Pair</param>
        /// <param name="amount">Amount of Base Currency to Buy</param>
        /// <param name="price">Counter Currency Purchase Price</param>
        /// <returns>BEx.Order</returns>
    
        public Order CreateBuyLimitOrder(TradingPair pair, decimal amount, decimal price)
        {
            var values = new Dictionary<StandardParameter, string>
            {
                {StandardParameter.Amount, amount.ToStringInvariant()},
                {StandardParameter.Price, price.ToStringInvariant()}
            };

            return executor.Execute(Commands.BuyOrder, pair, values);
        }

        /// <summary>
        ///     Create a Limit Order to sell "amount" of base currency at the counter currency "price" using the default trading
        ///     pair.
        /// </summary>
        /// <param name="amount">Amount of Base Currency to Sell</param>
        /// <param name="price">Counter Currency Sell Price</param>
        /// <returns>BEx.Order</returns>
     
        public Order CreateSellLimitOrder(decimal amount, decimal price)
        {
            return CreateSellLimitOrder(DefaultPair, amount, price);
        }

        /// <summary>
        ///     Create a Limit Order to sell "amount" of base currency at the counter currency "price" using the default trading
        ///     pair.
        /// </summary>
        /// <param name="pair">Trading Pair</param>
        /// <param name="amount">Amount of Base Currency to Sell</param>
        /// <param name="price">Counter Currency Sell Price</param>
        /// <returns>BEx.Order</returns>
     
        public Order CreateSellLimitOrder(TradingPair pair, decimal amount, decimal price)
        {
            var values = new Dictionary<StandardParameter, string>
            {
                {StandardParameter.Amount, amount.ToStringInvariant()},
                {StandardParameter.Price, price.ToStringInvariant()}
            };

            return executor.Execute(Commands.SellOrder, pair, values);
        }

        /// <summary>
        ///     Retrieve all Balance information for your account.
        /// </summary>
        /// <returns>BEx.AccountBalance</returns>
        public AccountBalance GetAccountBalance()
        {
            return executor.Execute(Commands.AccountBalance);
        }

        /// <summary>
        ///     Retrieve the your account Deposit Address for the requested currency
        /// </summary>
        /// <param name="toDeposit">CryptoCurrency to deposit</param>
        /// <returns>BEx.DepositAddress</returns>
        public DepositAddress GetDepositAddress(Currency toDeposit)
        {
            var pair = new TradingPair(toDeposit, toDeposit);

            return executor.Execute(Commands.DepositAddress, pair);
        }

        public OpenOrders GetOpenOrders()
        {
            return GetOpenOrders(DefaultPair);
        }

        public OpenOrders GetOpenOrders(TradingPair pair)
        {
            return executor.Execute(Commands.OpenOrders, pair);
        }

        /// <summary>
        ///     Url the current BTC/USD Order Book.
        /// </summary>
        /// <returns></returns>
        public OrderBook GetOrderBook()
        {
            return GetOrderBook(DefaultPair);
        }

        /// <summary>
        ///     Url the current Order Book for the specified Currency pair.
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        public OrderBook GetOrderBook(TradingPair pair)
        {
            return executor.Execute(Commands.OrderBook, pair);
        }

        /// <summary>
        ///     Retrieve the last Tick for the Exchange for the Default Trading Pair
        /// </summary>
        /// <returns>BEx.Tick</returns>
        public Tick GetTick()
        {
            return GetTick(DefaultPair);
        }

        /// <summary>
        ///     Retrieve the last Tick for the Exchange
        /// </summary>
        /// <param name="pair">Retrieve Tick for this Trading Pair</param>
        /// <returns>BEx.Tick</returns>
        public Tick GetTick(TradingPair pair)
        {
            return executor.Execute(Commands.Tick, pair);
        }

        /// <summary>
        ///     Return BTC/USD general Transactions for past hour.
        /// </summary>
        /// <returns></returns>
        public Transactions GetTransactions()
        {
            return GetTransactions(DefaultPair);
        }

        /// <summary>
        ///     Return general Transactions from the past hour for the specified currency pair.
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        public Transactions GetTransactions(TradingPair pair)
        {
            var values = new Dictionary<StandardParameter, string>
            {
                {
                    StandardParameter.UnixTimestamp,
                    DateTime.UtcNow.AddHours(-1).ToUnixTime().ToStringInvariant()
                }
            };

            return executor.Execute(Commands.Transactions, pair, values);
        }

        /// <summary>
        ///     Return your last 50 Order Transactions for the Default Trading Pair
        /// </summary>
        /// <returns>UserTransactions, non-null</returns>
        public UserTransactions GetUserTransactions()
        {
            return GetUserTransactions(DefaultPair);
        }

        /// <summary>
        ///     Return your last 50 Order Transactions for the Default Trading Pair
        /// </summary>
        /// <returns>UserTransactions, non-null</returns>
        public UserTransactions GetUserTransactions(TradingPair pair)
        {
            return executor.Execute(Commands.UserTransactions, pair);
        }

        /// <summary>
        ///     Verify that a currency pair (e.g. BTC/USD) is supported by this exchange.
        /// </summary>
        /// <param name="pair">Currency Pair</param>
        /// <returns>True if supported, otherwise false.</returns>
        public bool IsTradingPairSupported(TradingPair pair)
        {
            return Configuration.SupportedPairs.Contains(pair);
        }
    }
}