using System;
using System.Collections.Generic;
using BEx.ExchangeEngine.API;
using BEx.ExchangeEngine.API.Commands;
using BEx.Exchanges.Gdax.API.Models;
using RestSharp;

namespace BEx.Exchanges.Gdax.API
{
    internal class CommandFactory : IExchangeCommandFactory
    {
        private static readonly CommandFactory instance = new CommandFactory();

        private CommandFactory()
        {
            Build();

            var param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Url, "base", StandardParameter.Base),
                new ExchangeParameter(ParameterMethod.Url, "counter", StandardParameter.Counter)
            };

            Tick = new TickCommand(
                Method.GET,
                new Uri("products/{base}-{counter}/ticker", UriKind.Relative),
                false,
                typeof(TickModel),
                param);

            param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Url, "base", StandardParameter.Base),
                new ExchangeParameter(ParameterMethod.Url, "counter", StandardParameter.Counter)
            };

            DayRange = new DayRangeCommand(
                Method.GET,
                new Uri("products/{base}-{counter}/stats", UriKind.Relative),
                false,
                typeof(DayRangeModel),
                param);

            param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Url, "base", StandardParameter.Base),
                new ExchangeParameter(ParameterMethod.Url, "counter", StandardParameter.Counter)
            };

            OrderBook = new OrderBookCommand(
                Method.GET,
                new Uri("products/{base}-{counter}/book?level=2", UriKind.Relative),
                false,
                typeof(OrderBookModel),
                param);

            // /products/<product-id>/trades

            param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Url, "base", StandardParameter.Base),
                new ExchangeParameter(ParameterMethod.Url, "counter", StandardParameter.Counter)
            };

            Transactions = new TransactionsCommand(
                Method.GET,
                new Uri("products/{base}-{counter}/trades", UriKind.Relative),
                false,
                typeof(List<TransactionModel>),
                param);

            AccountBalance = new AccountBalanceCommand(
                Method.GET,
                new Uri("accounts", UriKind.Relative),
                true,
                typeof(List<AccountBalanceModel>));

            OpenOrders = new OpenOrdersCommand(
                Method.GET,
                new Uri("orders", UriKind.Relative),
                true,
                typeof(List<OpenOrderModel>));

            UserTransactions = new UserTransactionsCommand(
                Method.GET,
                new Uri("fills", UriKind.Relative),
                true,
                typeof(List<UserTransactionModel>));
        }

        public static IExchangeCommandFactory Singleton => instance;
        public AccountBalanceCommand AccountBalance { get; }
        public LimitOrderCommand BuyOrder { get; }
        public CancelOrderCommand CancelOrder { get; }
        public DayRangeCommand DayRange { get; }
        public OpenOrdersCommand OpenOrders { get; }
        public OrderBookCommand OrderBook { get; }
        public LimitOrderCommand SellOrder { get; }
        public TickCommand Tick { get; }
        public TransactionsCommand Transactions { get; }
        public UserTransactionsCommand UserTransactions { get; }

        private void Build()
        {
        }
    }
}