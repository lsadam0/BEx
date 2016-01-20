// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using BEx.ExchangeEngine.Bitfinex.JSON;
using BEx.ExchangeEngine.Commands;
using RestSharp;

namespace BEx.ExchangeEngine.Bitfinex
{
    internal class BitfinexCommandFactory : IExchangeCommandFactory
    {
        private static BitfinexCommandFactory instance = new BitfinexCommandFactory();

        private ExecutionEngine _engine;

        private BitfinexCommandFactory()
        {
            this.BuildCommands(new ExecutionEngine(new Uri("https://api.bitfinex.com"), ExchangeType.Bitfinex));
        }

        public static IExchangeCommandFactory Singleton
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// ExchangeCommand associated with IAuthenticatedCommands.GetAccountBalance()
        /// </summary>
        /// <returns></returns>
        public AccountBalanceCommand AccountBalance { get; private set; }

        /// <summary>
        /// ExchangeCommand associated with IAuthenticatedCommands.CreateBuyOrder()
        /// </summary>
        /// <returns></returns>
        public LimitOrderCommand BuyOrder { get; private set; }

        /// <summary>
        /// ExchangeCommand associated with IAuthenticatedCommands.CancelOrder()
        /// </summary>
        /// <returns></returns>
        public CancelOrderCommand CancelOrder { get; private set; }

        /// <summary>
        /// ExchangeCommand associated with IAuthenticatedCommands.GetDepositAddress()
        /// </summary>
        /// <returns></returns>
        public DepositAddressCommand DepositAddress { get; private set; }

        /// <summary>
        /// ExchangeCommand associated with IAuthenticatedCommands.GetOpenOrders()
        /// </summary>
        /// <returns></returns>
        public OpenOrdersCommand OpenOrders { get; private set; }

        /// <summary>
        /// ExchangeCommand associated with IUnauthenticatedCommands.GetOrderBook()
        /// </summary>
        /// <returns></returns>
        public OrderBookCommand OrderBook { get; private set; }

        /// <summary>
        /// ExchangeCommand associated with IAuthenticatedCommands.CreateSellOrder()
        /// </summary>
        /// <returns></returns>
        public LimitOrderCommand SellOrder { get; private set; }

        /// <summary>
        /// ExchangeCommand associated with IUnauthenticatedCommands.GetTick()
        /// </summary>
        /// <returns></returns>
        public TickCommand Tick { get; private set; }

        /// <summary>
        /// ExchangeCommand associated with IUnauthenticatedCommands.GetTransactions()
        /// </summary>
        /// <returns></returns>
        public TransactionsCommand Transactions { get; private set; }

        /// <summary>
        /// ExchangeCommand associated with IAuthenticatedCommands.GetUserTransactions()
        /// </summary>
        /// <returns></returns>
        public UserTransactionsCommand UserTransactions { get; private set; }

        public AccountBalanceCommand BuildAccountBalanceCommand()
        {
            return new AccountBalanceCommand(
                                _engine,
                                Method.POST,
                                new Uri("/v1/balances", UriKind.Relative),
                                true,
                                typeof(List<AccountBalanceIntermediate>));
        }

        public LimitOrderCommand BuildBuyOrderCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ParameterMethod.Post, "symbol", StandardParameter.Pair, "BTCUSD"),
                new ExchangeParameter(ParameterMethod.Post, "amount", StandardParameter.Amount),
                new ExchangeParameter(ParameterMethod.Post, "price", StandardParameter.Price),
                new ExchangeParameter(ParameterMethod.Post, "exchange", StandardParameter.None, "bitfinex"),
                new ExchangeParameter(ParameterMethod.Post, "type", StandardParameter.None, "exchange limit"),
                new ExchangeParameter(ParameterMethod.Post, "side", StandardParameter.None, "buy")
            };

            return new LimitOrderCommand(
                                _engine,
                                Method.POST,
                                new Uri("/v1/order/new", UriKind.Relative),
                                true,
                                typeof(OrderResponseIntermediate),
                                param);
        }

        public CancelOrderCommand BuildCancelOrderCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ParameterMethod.Post, "order_id", StandardParameter.Id)
            };

            return new CancelOrderCommand(
                                _engine,
                                Method.POST,
                                new Uri("/v1/order/cancel", UriKind.Relative),
                                true,
                                typeof(Confirmation),
                                param);
        }

        public void BuildCommands(ExecutionEngine executor)
        {
            _engine = executor;

            this.AccountBalance = BuildAccountBalanceCommand();
            this.BuyOrder = BuildBuyOrderCommand();
            this.CancelOrder = BuildCancelOrderCommand();
            this.DepositAddress = BuildDepositAddressCommand();
            this.OpenOrders = BuildOpenOrdersCommand();
            this.OrderBook = BuildOrderBookCommand();
            this.SellOrder = BuildSellOrderCommand();
            this.Tick = BuildTickCommand();
            this.Transactions = BuildTransactionsCommand();
            this.UserTransactions = BuildUserTransactionsCommand();
        }

        public DepositAddressCommand BuildDepositAddressCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ParameterMethod.Post, "currency", StandardParameter.Currency, "BTC"),
                new ExchangeParameter(ParameterMethod.Post, "method", StandardParameter.CurrencyFullName, "bitcoin")
                    {
                        IsLowercase = true
                    },
                new ExchangeParameter(ParameterMethod.Post, "wallet_name", StandardParameter.None, "exchange")
            };

            return new DepositAddressCommand(
                                _engine,
                                Method.POST,
                                new Uri("/v1/deposit/new", UriKind.Relative),
                                true,
                                typeof(DepositAddressIntermediate),
                                param);
        }

        public OpenOrdersCommand BuildOpenOrdersCommand()
        {
            return new OpenOrdersCommand(
                                _engine,
                                Method.POST,
                                new Uri("/v1/orders", UriKind.Relative),
                                true,
                                typeof(List<OrderResponseIntermediate>));
        }

        public OrderBookCommand BuildOrderBookCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ParameterMethod.Url, "pair", StandardParameter.Pair, "BTCUSD")
            };

            return new OrderBookCommand(
                _engine,
                Method.GET,
                new Uri("/v1/book/{pair}", UriKind.Relative),
                false,
                typeof(OrderBookIntermediate),
                param);
        }

        public LimitOrderCommand BuildSellOrderCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ParameterMethod.Post, "symbol", StandardParameter.Pair, "BTCUSD"),
                new ExchangeParameter(ParameterMethod.Post, "amount", StandardParameter.Amount),
                new ExchangeParameter(ParameterMethod.Post, "price", StandardParameter.Price),
                new ExchangeParameter(ParameterMethod.Post, "exchange", StandardParameter.None, "bitfinex"),
                new ExchangeParameter(ParameterMethod.Post, "type", StandardParameter.None, "exchange limit"),
                new ExchangeParameter(ParameterMethod.Post, "side", StandardParameter.None, "sell")
            };

            return new LimitOrderCommand(
                                _engine,
                                Method.POST,
                                new Uri("/v1/order/new", UriKind.Relative),
                                true,
                                typeof(OrderResponseIntermediate),
                                param);
        }

        public TickCommand BuildTickCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ParameterMethod.Url, "pair", StandardParameter.Pair)
            };

            return new TickCommand(
                _engine,
                Method.GET,
                new Uri("/v1/pubticker/{pair}", UriKind.Relative),
                false,
                typeof(TickIntermediate),
                param);
        }

        public TransactionsCommand BuildTransactionsCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ParameterMethod.Post, "timestamp", StandardParameter.UnixTimestamp, "needtoset"),
                new ExchangeParameter(ParameterMethod.Url, "pair", StandardParameter.Pair, "BTCUSD")
            };

            return new TransactionsCommand(
                _engine,
                Method.GET,
                new Uri("/v1/trades/{pair}", UriKind.Relative),
                false,
                typeof(List<TransactionIntermediate>),
                param);
        }

        public UserTransactionsCommand BuildUserTransactionsCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                 new ExchangeParameter(ParameterMethod.Post, "symbol", StandardParameter.Pair, "BTCUSD")
            };

            return new UserTransactionsCommand(
                                _engine,
                                Method.POST,
                                new Uri("/v1/mytrades", UriKind.Relative),
                                true,
                                typeof(List<UserTransactionIntermediate>),
                                param);
        }
    }
}