// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
                new ExchangeParameter(ParameterMethod.Post, "symbol", StandardParameter.Pair, "BTCUSD"),
                new ExchangeParameter(ParameterMethod.Post, "amount", StandardParameter.Amount),
                new ExchangeParameter(ParameterMethod.Post, "price", StandardParameter.Price),
                new ExchangeParameter(ParameterMethod.Post, "exchange", StandardParameter.None, "bitfinex"),
                new ExchangeParameter(ParameterMethod.Post, "type", StandardParameter.None, "exchange limit"),
                new ExchangeParameter(ParameterMethod.Post, "side", StandardParameter.None, "buy")
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
                new ExchangeParameter(ParameterMethod.Post, "order_id", StandardParameter.Id)
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
                new ExchangeParameter(ParameterMethod.Post, "currency", StandardParameter.Currency, "BTC"),
                new ExchangeParameter(ParameterMethod.Post, "method", StandardParameter.CurrencyFullName, "bitcoin")
                    {
                        IsLowercase = true
                    },
                new ExchangeParameter(ParameterMethod.Post, "wallet_name", StandardParameter.None, "exchange")
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
                new ExchangeParameter(ParameterMethod.Url, "pair", StandardParameter.Pair, "BTCUSD")
                
            };

            return new ExchangeCommand(
                CommandClass.OrderBook,
                Method.GET,
                new Uri("/v1/book/{pair}", UriKind.Relative),
                false,
                typeof(BitFinexOrderBookJSON));

        }

        public ExchangeCommand BuildSellOrderCommand()
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
                new ExchangeParameter(ParameterMethod.Url, "pair", StandardParameter.None)
            };

            return new ExchangeCommand(
                CommandClass.Tick,
                Method.GET,
                new Uri("/v1/pubticker/{pair}", UriKind.Relative),
                false,
                typeof(BitfinexTickJSON));
        }

        public ExchangeCommand BuildTransactionsCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                new ExchangeParameter(ParameterMethod.Post, "timestamp", StandardParameter.UnixTimestamp, "needtoset"),
                new ExchangeParameter(ParameterMethod.Url, "pair", StandardParameter.Pair, "BTCUSD")
                
            };

            return new ExchangeCommand(
                CommandClass.Transactions,
                Method.GET,
                new Uri("/v1/trades/{pair}", UriKind.Relative),
                false,
                typeof(List<BitFinexTransactionJSON>),
                param);
        }

        public ExchangeCommand BuildUserTransactionsCommand()
        {
            var param = new List<ExchangeParameter>()
            {
                { new ExchangeParameter(ParameterMethod.Post, "symbol", StandardParameter.Pair, "BTCUSD") }
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