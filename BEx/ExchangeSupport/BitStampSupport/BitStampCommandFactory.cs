﻿using BEx.CommandProcessing;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitStampCommandFactory : IExchangeCommandFactory
    {
        internal Dictionary<CommandClass, ExchangeCommand> collection;

        public BitStampCommandFactory()
        {
            collection = GetCommandCollection();
        }

        public ExchangeCommand BuildAccountBalanceCommand()
        {
            var accountBalance = new ExchangeCommand(CommandClass.AccountBalance,
                                                     Method.POST,
                                                     new Uri("balance/", UriKind.Relative),
                                                     true,
                                                     typeof(BitStampAccountBalanceJSON));

            return accountBalance;
        }

        public ExchangeCommand BuildBuyOrderCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "amount", StandardParameterType.Amount));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "price", StandardParameterType.Price));

            var buyOrder = new ExchangeCommand(CommandClass.BuyOrder,
                                                   Method.POST,
                                                   new Uri("buy/", UriKind.Relative),
                                                   true,
                                                   typeof(BitStampOrderConfirmationJSON),
                                                   param);

            return buyOrder;
        }

        public ExchangeCommand BuildCancelOrderCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "id", StandardParameterType.Id));

            var cancelOrder = new ExchangeCommand(CommandClass.CancelOrder,
                                                                Method.POST,
                                                                new Uri("cancel_order/", UriKind.Relative),
                                                                true,
                                                                typeof(Confirmation),
                                                                param);

            return cancelOrder;
        }

        public ExchangeCommand BuildDepositAddressCommand()
        {
            var depositAddress = new ExchangeCommand(CommandClass.DepositAddress,
                                                      Method.POST,
                                                      new Uri("bitcoin_deposit_address/", UriKind.Relative),
                                                      true,
                                                      typeof(string));

            return depositAddress;
        }

        public ExchangeCommand BuildOpenOrdersCommand()
        {
            var openOrders = new ExchangeCommand(CommandClass.OpenOrders,
                                                    Method.POST,
                                                    new Uri("open_orders/", UriKind.Relative),
                                                    true,
                                                    typeof(List<BitStampOpenOrdersJSON>)
                                                    );

            return openOrders;
        }

        public ExchangeCommand BuildOrderBookCommand()
        {
            var orderBook = new ExchangeCommand(CommandClass.OrderBook,
                                                                 Method.GET,
                                                                 new Uri("order_book/", UriKind.Relative),
                                                                 false,
                                                                 typeof(BitstampOrderBookJSON));

            return orderBook;
        }

        public ExchangeCommand BuildSellOrderCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "amount", StandardParameterType.Amount));

            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "price", StandardParameterType.Price));

            var sellOrder = new ExchangeCommand(CommandClass.SellOrder,
                                                    Method.POST,
                                                    new Uri("sell/", UriKind.Relative),
                                                    true,
                                                    typeof(BitStampOrderConfirmationJSON),
                                                    param);

            return sellOrder;
        }

        public ExchangeCommand BuildTickCommand()
        {
            var tick = new ExchangeCommand(CommandClass.Tick,
                                                      Method.GET,
                                                      new Uri("ticker/", UriKind.Relative),
                                                      false,
                                                      typeof(BitstampTickJSON));
            return tick;
        }

        public ExchangeCommand BuildTransactionsCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Address, "time", StandardParameterType.None, "hour"));

            var transactions = new ExchangeCommand(CommandClass.Transactions,
                                                                Method.GET,
                                                                new Uri("transactions/", UriKind.Relative),
                                                                false,
                                                                typeof(List<BitstampTransactionJSON>),
                                                                param);

            return transactions;
        }

        public ExchangeCommand BuildUserTransactionsCommand()
        {
            var userTransactions = new ExchangeCommand(CommandClass.UserTransactions,
                                                        Method.POST,
                                                        new Uri("user_transactions/", UriKind.Relative),
                                                        true,
                                                        typeof(List<BitStampUserTransactionJSON>));

            return userTransactions;
        }

        public ExchangeCommand GetCommand(CommandClass commandType)
        {
            return collection[commandType];
        }

        public Dictionary<CommandClass, ExchangeCommand> GetCommandCollection()
        {
            var res = new Dictionary<CommandClass, ExchangeCommand>();

            res.Add(CommandClass.AccountBalance, BuildAccountBalanceCommand());
            res.Add(CommandClass.BuyOrder, BuildBuyOrderCommand());
            res.Add(CommandClass.CancelOrder, BuildCancelOrderCommand());
            res.Add(CommandClass.DepositAddress, BuildDepositAddressCommand());
            res.Add(CommandClass.OpenOrders, BuildOpenOrdersCommand());
            res.Add(CommandClass.OrderBook, BuildOrderBookCommand());
            res.Add(CommandClass.SellOrder, BuildSellOrderCommand());
            res.Add(CommandClass.Tick, BuildTickCommand());
            res.Add(CommandClass.Transactions, BuildTransactionsCommand());
            res.Add(CommandClass.UserTransactions, BuildUserTransactionsCommand());

            return res;
        }

        public IList<ExchangeCommand> GetCommands()
        {
            return collection.Values.ToList();
        }
    }
}