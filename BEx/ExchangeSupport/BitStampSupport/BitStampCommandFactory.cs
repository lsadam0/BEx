using System;
using System.Collections.Generic;
using System.Linq;
using BEx.CommandProcessing;
using RestSharp;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitStampCommandFactory : IExchangeCommandFactory
    {

        private readonly Dictionary<CommandClass, ExchangeCommand> commandCollection;


        public BitStampCommandFactory()
        {
            commandCollection = GetCommandCollection();
        }
        public ExchangeCommand BuildAccountBalanceCommand()
        {
            return new ExchangeCommand(
                                CommandClass.AccountBalance,
                                Method.POST,
                                new Uri("balance/", UriKind.Relative),
                                true,
                                typeof(BitStampAccountBalanceJSON));
        }

        public ExchangeCommand BuildBuyOrderCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ExchangeParameterType.Post, "amount", StandardParameterType.Amount),
                new ExchangeParameter(ExchangeParameterType.Post, "price", StandardParameterType.Price)
            };

            return new ExchangeCommand(
                                CommandClass.BuyOrder,
                                Method.POST,
                                new Uri("buy/", UriKind.Relative),
                                true,
                                typeof(BitStampOrderConfirmationJSON),
                                param);
        }

        public ExchangeCommand BuildCancelOrderCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ExchangeParameterType.Post, "id", StandardParameterType.Id)
            };

            return new ExchangeCommand(
                                CommandClass.CancelOrder,
                                Method.POST,
                                new Uri("cancel_order/", UriKind.Relative),
                                true,
                                typeof(Confirmation),
                                param);
        }

        public ExchangeCommand BuildDepositAddressCommand()
        {
            return new ExchangeCommand(
                                CommandClass.DepositAddress,
                                Method.POST,
                                new Uri("bitcoin_deposit_address/", UriKind.Relative),
                                true,
                                typeof(string));
        }

        public ExchangeCommand BuildOpenOrdersCommand()
        {
            return new ExchangeCommand(
                                CommandClass.OpenOrders,
                                Method.POST,
                                new Uri("open_orders/", UriKind.Relative),
                                true,
                                typeof(List<BitStampOpenOrdersJSON>));
        }

        public ExchangeCommand BuildOrderBookCommand()
        {
            return new ExchangeCommand(
                                CommandClass.OrderBook,
                                Method.GET,
                                new Uri("order_book/", UriKind.Relative),
                                false,
                                typeof(BitstampOrderBookJSON));
        }

        public ExchangeCommand BuildSellOrderCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ExchangeParameterType.Post, "amount", StandardParameterType.Amount),
                new ExchangeParameter(ExchangeParameterType.Post, "price", StandardParameterType.Price)
            };

            return new ExchangeCommand(
                                CommandClass.SellOrder,
                                Method.POST,
                                new Uri("sell/", UriKind.Relative),
                                true,
                                typeof(BitStampOrderConfirmationJSON),
                                param);
        }

        public ExchangeCommand BuildTickCommand()
        {
            return new ExchangeCommand(
                                CommandClass.Tick,
                                Method.GET,
                                new Uri("ticker/", UriKind.Relative),
                                false,
                                typeof(BitstampTickJSON));
        }

        public ExchangeCommand BuildTransactionsCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ExchangeParameterType.Address, "time", StandardParameterType.None, "hour")
            };

            return new ExchangeCommand(
                                CommandClass.Transactions,
                                Method.GET,
                                new Uri("transactions/", UriKind.Relative),
                                false,
                                typeof(List<BitstampTransactionJSON>),
                                param);
        }

        public ExchangeCommand BuildUserTransactionsCommand()
        {
            return new ExchangeCommand(
                                CommandClass.UserTransactions,
                                Method.POST,
                                new Uri("user_transactions/", UriKind.Relative),
                                true,
                                typeof(List<BitStampUserTransactionJSON>));
        }

        public ExchangeCommand GetCommand(CommandClass commandType)
        {
            return commandCollection[commandType];
        }

        public Dictionary<CommandClass, ExchangeCommand> GetCommandCollection()
        {
            var res = new Dictionary<CommandClass, ExchangeCommand>()
            {
                { CommandClass.AccountBalance, BuildAccountBalanceCommand() },
                { CommandClass.BuyOrder, BuildBuyOrderCommand() },
                { CommandClass.CancelOrder, BuildCancelOrderCommand() },
                { CommandClass.DepositAddress, BuildDepositAddressCommand() },
                { CommandClass.OpenOrders, BuildOpenOrdersCommand() },
                { CommandClass.OrderBook, BuildOrderBookCommand() },
                { CommandClass.SellOrder, BuildSellOrderCommand() },
                { CommandClass.Tick, BuildTickCommand() },
                { CommandClass.Transactions, BuildTransactionsCommand() },
                { CommandClass.UserTransactions, BuildUserTransactionsCommand() }
            };

            return res;
        }

        
    }
}