// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.API;
using BEx.ExchangeEngine.API.Commands;
using BEx.ExchangeEngine.Utilities;
using BEx.Response;

namespace BEx
{
    public abstract class Exchange : IExchange, IDisposable
    {
        private readonly IExchangeCommandFactory _commands;
        private readonly IExchangeConfiguration _configuration;
        private readonly ExecutionEngine _executor;
        protected SocketObservable _socketObservable;
        protected SocketObserver _socketObserver;

        private bool disposedValue;

        internal Exchange(
            IExchangeConfiguration configuration,
            IExchangeCommandFactory commands)
        {
            _configuration = configuration;
            _commands = commands;
            SetupSocket();

            _executor = new ExecutionEngine(
                configuration.BaseUri,
                configuration.ExchangeSourceType);
        }

        internal Exchange(
            IExchangeConfiguration configuration,
            IExchangeCommandFactory commands,
            IExchangeAuthenticator authenticator)
        {
            _configuration = configuration;
            _commands = commands;
            SetupSocket();

            _executor = new ExecutionEngine(
                configuration.BaseUri,
                authenticator,
                configuration.ExchangeSourceType);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public TradingPair DefaultPair => _configuration.DefaultPair;

        public ExchangeType ExchangeSourceType => _configuration.ExchangeSourceType;

        public ImmutableHashSet<Currency> SupportedCurrencies => _configuration.SupportedCurrencies;

        public ImmutableHashSet<TradingPair> SupportedTradingPairs => _configuration.SupportedPairs;

        public Confirmation CancelOrder(Order toCancel) => CancelOrder(toCancel.Id);

        public Confirmation CancelOrder(string id)
        {
            var values = new Dictionary<StandardParameter, string>
            {
                {StandardParameter.Id, id}
            };

            return _executor.Execute<Confirmation>(
                _commands.CancelOrder,
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
            => CreateBuyLimitOrder(DefaultPair, amount, price);

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

            return _executor.Execute<Order>(_commands.BuyOrder, pair, values);
        }

        /// <summary>
        ///     Create a Limit Order to sell "amount" of base currency at the counter currency "price" using the default trading
        ///     pair.
        /// </summary>
        /// <param name="amount">Amount of Base Currency to Sell</param>
        /// <param name="price">Counter Currency Sell Price</param>
        /// <returns>BEx.Order</returns>
        public Order CreateSellLimitOrder(decimal amount, decimal price)
            => CreateSellLimitOrder(DefaultPair, amount, price);

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

            return _executor.Execute<Order>(_commands.SellOrder, pair, values);
        }

        /// <summary>
        ///     Retrieve all Balance information for your account.
        /// </summary>
        /// <returns>BEx.AccountBalance</returns>
        public AccountBalance GetAccountBalance() => _executor.Execute<AccountBalance>(_commands.AccountBalance);

        public OpenOrders GetOpenOrders() => GetOpenOrders(DefaultPair);

        public OpenOrders GetOpenOrders(TradingPair pair) => _executor.Execute<OpenOrders>(_commands.OpenOrders, pair);

        /// <summary>
        ///     Url the current BTC/USD Order Book.
        /// </summary>
        /// <returns></returns>
        public OrderBook GetOrderBook() => GetOrderBook(DefaultPair);

        /// <summary>
        ///     Url the current Order Book for the specified Currency pair.
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        public OrderBook GetOrderBook(TradingPair pair) => _executor.Execute<OrderBook>(_commands.OrderBook, pair);

        /// <summary>
        ///     Retrieve the last Tick for the Exchange for the Default Trading Pair
        /// </summary>
        /// <returns>BEx.Tick</returns>
        public Tick GetTick() => GetTick(DefaultPair);

        /// <summary>
        ///     Retrieve the last Tick for the Exchange
        /// </summary>
        /// <param name="pair">Retrieve Tick for this Trading Pair</param>
        /// <returns>BEx.Tick</returns>
        public Tick GetTick(TradingPair pair) => _executor.Execute<Tick>(_commands.Tick, pair);

        /// <summary>
        ///     Return BTC/USD general Transactions for past hour.
        /// </summary>
        /// <returns></returns>
        public Transactions GetTransactions() => GetTransactions(DefaultPair);

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

            return _executor.Execute<Transactions>(_commands.Transactions, pair, values);
        }

        /// <summary>
        ///     Return your last 50 Order Transactions for the Default Trading Pair
        /// </summary>
        /// <returns>UserTransactions, non-null</returns>
        public UserTransactions GetUserTransactions() => GetUserTransactions(DefaultPair);

        /// <summary>
        ///     Return your last 50 Order Transactions for the Default Trading Pair
        /// </summary>
        /// <returns>UserTransactions, non-null</returns>
        public UserTransactions GetUserTransactions(TradingPair pair)
            => _executor.Execute<UserTransactions>(_commands.UserTransactions, pair);

        /// <summary>
        ///     Verify that a currency pair (e.g. BTC/USD) is supported by this exchange.
        /// </summary>
        /// <param name="pair">Currency Pair</param>
        /// <returns>True if supported, otherwise false.</returns>
        public bool IsTradingPairSupported(TradingPair pair) => _configuration.SupportedPairs.Contains(pair);

        private void SetupSocket()
        {
            Subscribe();
        }

        protected abstract void Subscribe();

        public DayRange Get24HrStats(TradingPair pair)
        {
            return _executor.Execute<DayRange>(_commands.DayRange, pair);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _socketObservable?.Dispose();
                }

                disposedValue = true;
            }
        }
    }
}