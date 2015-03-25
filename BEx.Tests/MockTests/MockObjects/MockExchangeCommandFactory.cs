using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEx.ExchangeEngine;
using BEx.ExchangeEngine.Commands;
using BEx.ExchangeEngine.BitfinexSupport;
using RestSharp;

namespace BEx.UnitTests.MockTests.MockObjects
{
    internal class MockExchangeCommandFactory : IExchangeCommandFactory
    {
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

        private ExecutionEngine _engine;

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

        public AccountBalanceCommand BuildAccountBalanceCommand()
        {
            return new AccountBalanceCommand(
                                _engine,
                                Method.POST,
                                new Uri("/v1/balances", UriKind.Relative),
                                true,
                                typeof(List<BitFinexAccountBalanceJSON>));
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
                                typeof(BitFinexOrderResponseJSON),
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
                                typeof(BitFinexDepositAddressJSON),
                                param);
        }

        public OpenOrdersCommand BuildOpenOrdersCommand()
        {
            return new OpenOrdersCommand(
                                _engine,
                                Method.POST,
                                new Uri("/v1/orders", UriKind.Relative),
                                true,
                                typeof(List<BitFinexOrderResponseJSON>));
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
                typeof(BitFinexOrderBookJSON),
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
                                typeof(BitFinexOrderResponseJSON),
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
                typeof(BitfinexTickJSON),
                param);
        }

        public TransactionsCommand BuildTransactionsCommand()
        {
            /*timestamp (time): Optional. Only show trades at or after this timestamp.
limit_trades (int): Optional. Limit the number of trades returned. Must be >= 1. Default is 50.*/
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
                typeof(List<BitFinexTransactionJSON>),
                param);
        }

        public UserTransactionsCommand BuildUserTransactionsCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                 new ExchangeParameter(ParameterMethod.Post, "symbol", StandardParameter.Pair, "BTCUSD")
                // new ExchangeParameter(ParameterMethod.Post, "limit_trades", StandardParameter.Limit, "50")
            };

            return new UserTransactionsCommand(
                                _engine,
                                Method.POST,
                                new Uri("/v1/mytrades", UriKind.Relative),
                                true,
                                typeof(List<BitFinexUserTransactionJSON>),
                                param);
        }
    }
}
