using System;
using System.Collections.Generic;
using System.Linq;
using BEx.ExchangeEngine;
using RestSharp;


namespace BEx.ExchangeEngine.BitfinexSupport
{
    internal class BitfinexCommandFactory : IExchangeCommandFactory
    {

        private readonly Dictionary<CommandClass, ExchangeCommand> commandCollection;

        public BitfinexCommandFactory()
        {
            commandCollection = GetCommandCollection();
        }

        public ExchangeCommand BuildAccountBalanceCommand()
        {
            return new ExchangeCommand(
                                CommandClass.AccountBalance,
                                Method.POST,
                                new Uri("/v1/balances", UriKind.Relative),
                                true,
                                typeof(List<BitFinexAccountBalanceJSON>));
        }

        public ExchangeCommand BuildBuyOrderCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ExchangeParameterType.Post, "symbol", StandardParameterType.Pair, "BTCUSD"),
                new ExchangeParameter(ExchangeParameterType.Post, "amount", StandardParameterType.Amount),
                new ExchangeParameter(ExchangeParameterType.Post, "price", StandardParameterType.Price),
                new ExchangeParameter(ExchangeParameterType.Post, "exchange", StandardParameterType.None, "bitfinex"),
                new ExchangeParameter(ExchangeParameterType.Post, "type", StandardParameterType.None, "exchange limit"),
                new ExchangeParameter(ExchangeParameterType.Post, "side", StandardParameterType.None, "buy")
            };

            return new ExchangeCommand(
                                CommandClass.BuyOrder,
                                Method.POST,
                                new Uri("/v1/order/new", UriKind.Relative),
                                true,
                                typeof(BitFinexOrderResponseJSON),
                                param);
        }

        public ExchangeCommand BuildCancelOrderCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ExchangeParameterType.Post, "order_id", StandardParameterType.Id)
            };

            return new ExchangeCommand(
                                CommandClass.CancelOrder,
                                Method.POST,
                                new Uri("/v1/order/cancel", UriKind.Relative),
                                true,
                                typeof(Confirmation),
                                param);
        }

        public ExchangeCommand BuildDepositAddressCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ExchangeParameterType.Post, "currency", StandardParameterType.Currency, "BTC"),
                new ExchangeParameter(ExchangeParameterType.Post, "method", StandardParameterType.CurrencyFullName, "bitcoin")
                    {
                        IsLowercase = true
                    },
                new ExchangeParameter(ExchangeParameterType.Post, "wallet_name", StandardParameterType.None, "exchange")
            };

            return new ExchangeCommand(
                                CommandClass.DepositAddress,
                                Method.POST,
                                new Uri("/v1/deposit/new", UriKind.Relative),
                                true,
                                typeof(BitFinexDepositAddressJSON),
                                param);
        }

        public ExchangeCommand BuildOpenOrdersCommand()
        {
            return new ExchangeCommand(
                                CommandClass.OpenOrders,
                                Method.POST,
                                new Uri("/v1/orders", UriKind.Relative),
                                true,
                                typeof(List<BitFinexOrderResponseJSON>));
        }

        public ExchangeCommand BuildOrderBookCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ExchangeParameterType.Address, "base", StandardParameterType.Base, "BTC"),
                new ExchangeParameter(ExchangeParameterType.Address, "counter", StandardParameterType.Counter, "USD")
            };

            return new ExchangeCommand(
                                CommandClass.OrderBook,
                                Method.GET,
                                new Uri("/v1/book/{0}{1}", UriKind.Relative),
                                false,
                                typeof(BitFinexOrderBookJSON),
                                param);
        }

        public ExchangeCommand BuildSellOrderCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ExchangeParameterType.Post, "symbol", StandardParameterType.Pair, "BTCUSD"),
                new ExchangeParameter(ExchangeParameterType.Post, "amount", StandardParameterType.Amount),
                new ExchangeParameter(ExchangeParameterType.Post, "price", StandardParameterType.Price),
                new ExchangeParameter(ExchangeParameterType.Post, "exchange", StandardParameterType.None, "bitfinex"),
                new ExchangeParameter(ExchangeParameterType.Post, "type", StandardParameterType.None, "exchange limit"),
                new ExchangeParameter(ExchangeParameterType.Post, "side", StandardParameterType.None, "sell")
            };

            return new ExchangeCommand(
                                CommandClass.SellOrder,
                                Method.POST,
                                new Uri("/v1/order/new", UriKind.Relative),
                                true,
                                typeof(BitFinexOrderResponseJSON),
                                param);
        }

        public ExchangeCommand BuildTickCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ExchangeParameterType.Address, "pair", StandardParameterType.None)
            };

            return new ExchangeCommand(
                                CommandClass.Tick,
                                Method.GET,
                                new Uri("/v1/pubticker/{0}{1}", UriKind.Relative),
                                false,
                                typeof(BitfinexTickJSON),
                                param);
        }

        public ExchangeCommand BuildTransactionsCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ExchangeParameterType.Post, "timestamp", StandardParameterType.UnixTimestamp, "needtoset"),
                new ExchangeParameter(ExchangeParameterType.Address, "base", StandardParameterType.Base, "BTC")
                {
                    IsLowercase = true
                },
                new ExchangeParameter(ExchangeParameterType.Address, "counter", StandardParameterType.Counter, "USD")
                {
                    IsLowercase = true
                }
            };

            return new ExchangeCommand(
                                CommandClass.Transactions,
                                Method.GET,
                                new Uri("/v1/trades/{0}{1}", UriKind.Relative),
                                false,
                                typeof(List<BitFinexTransactionJSON>),
                                param)
            {
                LowercaseUrlParameters = true
            };
        }

        public ExchangeCommand BuildUserTransactionsCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                { new ExchangeParameter(ExchangeParameterType.Post, "symbol", StandardParameterType.Pair, "BTCUSD") }
            };

            return new ExchangeCommand(
                                CommandClass.UserTransactions,
                                Method.POST,
                                new Uri("/v1/mytrades", UriKind.Relative),
                                true,
                                typeof(List<BitFinexUserTransactionJSON>),
                                param);
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
                { CommandClass.UserTransactions, BuildUserTransactionsCommand() },
            };

            return res;
        }


    }
}