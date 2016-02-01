using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEx.ExchangeEngine.Commands;
using RestSharp;

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
            typeof(string), 
            param);
        }

        private void Build()
        {



        }

        public static IExchangeCommandFactory Singleton => instance;
        public AccountBalanceCommand AccountBalance { get; }
        public LimitOrderCommand BuyOrder { get; }
        public CancelOrderCommand CancelOrder { get; }
        public DepositAddressCommand DepositAddress { get; }
        public OpenOrdersCommand OpenOrders { get; }
        public OrderBookCommand OrderBook { get; }
        public LimitOrderCommand SellOrder { get; }
        public TickCommand Tick { get; }
        public TransactionsCommand Transactions { get; }
        public UserTransactionsCommand UserTransactions { get; }
    }
}
