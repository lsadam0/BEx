// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

using BEx.ExchangeEngine.API;
using BEx.ExchangeEngine.API.Commands;
using BEx.Exchanges.Bitfinex.API.Models;
using RestSharp;

namespace BEx.Exchanges.Bitfinex.API
{
    internal class CommandFactory : IExchangeCommandFactory
    {
        private static readonly CommandFactory Instance = new CommandFactory();

        private CommandFactory()
        {
            BuildCommands();
        }

        public static IExchangeCommandFactory Singleton => Instance;

        /// <summary>
        ///     ExchangeCommand associated with IAuthenticatedCommands.GetAccountBalance()
        /// </summary>
        /// <returns></returns>
        public AccountBalanceCommand AccountBalance { get; private set; }

        /// <summary>
        ///     ExchangeCommand associated with IAuthenticatedCommands.CreateBuyOrder()
        /// </summary>
        /// <returns></returns>
        public LimitOrderCommand BuyOrder { get; private set; }

        /// <summary>
        ///     ExchangeCommand associated with IAuthenticatedCommands.CancelOrder()
        /// </summary>
        /// <returns></returns>
        public CancelOrderCommand CancelOrder { get; private set; }

        public DayRangeCommand DayRange { get; private set; }

        /// <summary>
        ///     ExchangeCommand associated with IAuthenticatedCommands.GetOpenOrders()
        /// </summary>
        /// <returns></returns>
        public OpenOrdersCommand OpenOrders { get; private set; }

        /// <summary>
        ///     ExchangeCommand associated with IUnauthenticatedCommands.GetOrderBook()
        /// </summary>
        /// <returns></returns>
        public OrderBookCommand OrderBook { get; private set; }

        /// <summary>
        ///     ExchangeCommand associated with IAuthenticatedCommands.CreateSellOrder()
        /// </summary>
        /// <returns></returns>
        public LimitOrderCommand SellOrder { get; private set; }

        /// <summary>
        ///     ExchangeCommand associated with IUnauthenticatedCommands.GetTick()
        /// </summary>
        /// <returns></returns>
        public TickCommand Tick { get; private set; }

        /// <summary>
        ///     ExchangeCommand associated with IUnauthenticatedCommands.GetTransactions()
        /// </summary>
        /// <returns></returns>
        public TransactionsCommand Transactions { get; private set; }

        /// <summary>
        ///     ExchangeCommand associated with IAuthenticatedCommands.GetUserTransactions()
        /// </summary>
        /// <returns></returns>
        public UserTransactionsCommand UserTransactions { get; private set; }

        public AccountBalanceCommand BuildAccountBalanceCommand()
        {
            return new AccountBalanceCommand(
                Method.POST,
                new Uri("/v1/balances", UriKind.Relative),
                true,
                typeof(List<AccountBalanceModel>));
        }

        public LimitOrderCommand BuildBuyOrderCommand()
        {
            var param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Post, "symbol", StandardParameter.Pair, "BTCUSD"),
                new ExchangeParameter(ParameterMethod.Post, "amount", StandardParameter.Amount),
                new ExchangeParameter(ParameterMethod.Post, "price", StandardParameter.Price),
                new ExchangeParameter(ParameterMethod.Post, "exchange", StandardParameter.None, "bitfinex"),
                new ExchangeParameter(ParameterMethod.Post, "type", StandardParameter.None, "exchange limit"),
                new ExchangeParameter(ParameterMethod.Post, "side", StandardParameter.None, "buy")
            };

            return new LimitOrderCommand(
                Method.POST,
                new Uri("/v1/order/new", UriKind.Relative),
                true,
                typeof(OrderResponseModel),
                param);
        }

        public CancelOrderCommand BuildCancelOrderCommand()
        {
            var param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Post, "order_id", StandardParameter.Id)
            };

            return new CancelOrderCommand(
                Method.POST,
                new Uri("/v1/order/cancel", UriKind.Relative),
                true,
                typeof(Confirmation),
                param);
        }

        public void BuildCommands()
        {
            AccountBalance = BuildAccountBalanceCommand();
            BuyOrder = BuildBuyOrderCommand();
            CancelOrder = BuildCancelOrderCommand();

            OpenOrders = BuildOpenOrdersCommand();
            OrderBook = BuildOrderBookCommand();
            SellOrder = BuildSellOrderCommand();
            Tick = BuildTickCommand();
            Transactions = BuildTransactionsCommand();
            UserTransactions = BuildUserTransactionsCommand();
            DayRange = BuildDayRangeCommand();
        }

        public OpenOrdersCommand BuildOpenOrdersCommand()
        {
            return new OpenOrdersCommand(
                Method.POST,
                new Uri("/v1/orders", UriKind.Relative),
                true,
                typeof(List<OrderResponseModel>));
        }

        public OrderBookCommand BuildOrderBookCommand()
        {
            var param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Url, "pair", StandardParameter.Pair, "BTCUSD")
            };

            return new OrderBookCommand(
                Method.GET,
                new Uri("/v1/book/{pair}", UriKind.Relative),
                false,
                typeof(OrderBookModel),
                param);
        }

        public LimitOrderCommand BuildSellOrderCommand()
        {
            var param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Post, "symbol", StandardParameter.Pair, "BTCUSD"),
                new ExchangeParameter(ParameterMethod.Post, "amount", StandardParameter.Amount),
                new ExchangeParameter(ParameterMethod.Post, "price", StandardParameter.Price),
                new ExchangeParameter(ParameterMethod.Post, "exchange", StandardParameter.None, "bitfinex"),
                new ExchangeParameter(ParameterMethod.Post, "type", StandardParameter.None, "exchange limit"),
                new ExchangeParameter(ParameterMethod.Post, "side", StandardParameter.None, "sell")
            };

            return new LimitOrderCommand(
                Method.POST,
                new Uri("/v1/order/new", UriKind.Relative),
                true,
                typeof(OrderResponseModel),
                param);
        }

        public TickCommand BuildTickCommand()
        {
            var param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Url, "pair", StandardParameter.Pair)
            };

            return new TickCommand(
                Method.GET,
                new Uri("/v1/pubticker/{pair}", UriKind.Relative),
                false,
                typeof(TickModel),
                param);
        }

        public DayRangeCommand BuildDayRangeCommand()
        {
            var param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Url, "pair", StandardParameter.Pair)
            };

            return new DayRangeCommand(
                Method.GET,
                new Uri("/v1/pubticker/{pair}", UriKind.Relative),
                false,
                typeof(DayRangeModel),
                param);
        }

        public TransactionsCommand BuildTransactionsCommand()
        {
            var param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Post, "timestamp", StandardParameter.UnixTimestamp, "needtoset"),
                new ExchangeParameter(ParameterMethod.Url, "pair", StandardParameter.Pair, "BTCUSD")
            };

            return new TransactionsCommand(
                Method.GET,
                new Uri("/v1/trades/{pair}", UriKind.Relative),
                false,
                typeof(List<TransactionModel>),
                param);
        }

        public UserTransactionsCommand BuildUserTransactionsCommand()
        {
            var param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Post, "symbol", StandardParameter.Pair, "BTCUSD")
            };

            return new UserTransactionsCommand(
                Method.POST,
                new Uri("/v1/mytrades", UriKind.Relative),
                true,
                typeof(List<UserTransactionModel>),
                param);
        }
    }
}