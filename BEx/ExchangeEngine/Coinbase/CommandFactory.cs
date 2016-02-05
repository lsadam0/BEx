using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEx.ExchangeEngine.Commands;
using RestSharp;
using BEx.ExchangeEngine.Coinbase.JSON;

namespace BEx.ExchangeEngine.Coinbase
{
    internal class CommandFactory : IExchangeCommandFactory
    {
        private static readonly CommandFactory instance = new CommandFactory();

        private CommandFactory()
        {

            this.Build();


            var param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Url, "base", StandardParameter.Base),
                new ExchangeParameter(ParameterMethod.Url, "counter", StandardParameter.Counter)
            };

            this.Tick = new TickCommand(
               Method.GET,
               new Uri("products/{base}-{counter}/ticker", UriKind.Relative),
               false,
               typeof(TickIntermediate),
               param);

            param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Url, "base", StandardParameter.Base),
                new ExchangeParameter(ParameterMethod.Url, "counter", StandardParameter.Counter)
            };

            this.DayRange = new DayRangeCommand(
               Method.GET,
               new Uri("products/{base}-{counter}/stats", UriKind.Relative),
               false,
               typeof(DayRangeIntermediate),
               param);

            param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Url, "base", StandardParameter.Base),
                new ExchangeParameter(ParameterMethod.Url, "counter", StandardParameter.Counter)
            };

            this.OrderBook = new OrderBookCommand(
                Method.GET,
                new Uri("products/{base}-{counter}/book?level=2", UriKind.Relative),
                false,
                typeof(OrderBookIntermediate),
                param);

            // /products/<product-id>/trades

            param = new List<ExchangeParameter>
            {
                new ExchangeParameter(ParameterMethod.Url, "base", StandardParameter.Base),
                new ExchangeParameter(ParameterMethod.Url, "counter", StandardParameter.Counter)
            };

            this.Transactions = new TransactionsCommand(
                Method.GET,
                new Uri("products/{base}-{counter}/trades", UriKind.Relative),
                false,
                typeof(List<TransactionIntermediate>),
                param);

            this.AccountBalance = new AccountBalanceCommand(
                Method.GET,
                new Uri("accounts", UriKind.Relative),
                true,
                typeof(List<AccountBalanceIntermediate>));
        }

        private void Build()
        {



        }

        public static IExchangeCommandFactory Singleton => instance;
        public AccountBalanceCommand AccountBalance { get; }
        public LimitOrderCommand BuyOrder { get; }
        public CancelOrderCommand CancelOrder { get; }
        public DayRangeCommand DayRange { get; }
        public DepositAddressCommand DepositAddress { get; }
        public OpenOrdersCommand OpenOrders { get; }
        public OrderBookCommand OrderBook { get; }
        public LimitOrderCommand SellOrder { get; }
        public TickCommand Tick { get; }
        public TransactionsCommand Transactions { get; }
        public UserTransactionsCommand UserTransactions { get; }
    }
}
