using BEx.CommandProcessing;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BEx.ExchangeSupport.BitfinexSupport
{
    internal class BitfinexCommandFactory : IExchangeCommandFactory
    {
        private Dictionary<CommandClass, ExchangeCommand> collection;

        public BitfinexCommandFactory()
        {
            collection = GetCommandCollection();
        }

        public ExchangeCommand BuildAccountBalanceCommand()
        {
            var accountBalance = new ExchangeCommand(CommandClass.AccountBalance,
                                                        Method.POST,
                                                        new Uri("/v1/balances", UriKind.Relative),
                                                        true,
                                                        typeof(List<BitFinexAccountBalanceJSON>));

            return accountBalance;
        }

        public ExchangeCommand BuildBuyOrderCommand()
        {
            var param = new List<ExchangeParameter>();
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "symbol", StandardParameterType.Pair, "BTCUSD"));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "amount", StandardParameterType.Amount));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "price", StandardParameterType.Price));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "exchange", StandardParameterType.None, "bitfinex"));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "type", StandardParameterType.None, "exchange limit"));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "side", StandardParameterType.None, "buy"));

            var buyOrder = new ExchangeCommand(CommandClass.BuyOrder,
                                                Method.POST,
                                                new Uri("/v1/order/new", UriKind.Relative),
                                                true,
                                                typeof(BitFinexOrderResponseJSON),
                                                param);

            return buyOrder;
        }

        public ExchangeCommand BuildCancelOrderCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "order_id", StandardParameterType.Id));

            var cancelOrder = new ExchangeCommand(CommandClass.CancelOrder,
                                                                Method.POST,
                                                                new Uri("/v1/order/cancel", UriKind.Relative),
                                                                true,
                                                                typeof(Confirmation),
                                                                param);

            return cancelOrder;
        }

        public ExchangeCommand BuildDepositAddressCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "currency", StandardParameterType.Currency, "BTC"));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "method", StandardParameterType.CurrencyFullName, "bitcoin") { IsLowercase = true });
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "wallet_name", StandardParameterType.None, "exchange"));

            var depositAddress = new ExchangeCommand(CommandClass.DepositAddress,
                                                         Method.POST,
                                                         new Uri("/v1/deposit/new", UriKind.Relative),
                                                         true,
                                                         typeof(BitFinexDepositAddressJSON),
                                                         param);

            return depositAddress;
        }

        public ExchangeCommand BuildOpenOrdersCommand()
        {
            var openOrders = new ExchangeCommand(CommandClass.OpenOrders,
                                                  Method.POST,
                                                  new Uri("/v1/orders", UriKind.Relative),
                                                  true,
                                                  typeof(List<BitFinexOrderResponseJSON>));

            return openOrders;
        }

        public ExchangeCommand BuildOrderBookCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Address, "base", StandardParameterType.Base, "BTC"));
            param.Add(new ExchangeParameter(ExchangeParameterType.Address, "counter", StandardParameterType.Counter, "USD"));

            var orderBook = new ExchangeCommand(CommandClass.OrderBook,
                                                 Method.GET,
                                                 new Uri("/v1/book/{0}{1}", UriKind.Relative),
                                                 false,
                                                 typeof(BitFinexOrderBookJSON),
                                                 param);

            return orderBook;
        }

        public ExchangeCommand BuildSellOrderCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "symbol", StandardParameterType.Pair, "BTCUSD"));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "amount", StandardParameterType.Amount));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "price", StandardParameterType.Price));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "exchange", StandardParameterType.None, "bitfinex"));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "type", StandardParameterType.None, "exchange limit"));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "side", StandardParameterType.None, "sell"));

            var sell = new ExchangeCommand(CommandClass.SellOrder,
                                               Method.POST,
                                               new Uri("/v1/order/new", UriKind.Relative),
                                               true,
                                               typeof(BitFinexOrderResponseJSON),
                                               param);

            return sell;
        }

        public ExchangeCommand BuildTickCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Address, "pair", StandardParameterType.None));

            var tick = new ExchangeCommand(CommandClass.Tick,
                                            Method.GET,
                                            new Uri("/v1/pubticker/{0}{1}", UriKind.Relative),
                                            false,
                                            typeof(BitfinexTickJSON),
                                            param);
            return tick;
        }

        public ExchangeCommand BuildTransactionsCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "timestamp", StandardParameterType.UnixTimestamp, "needtoset"));

            ExchangeParameter baseTrans = new ExchangeParameter(ExchangeParameterType.Address, "base", StandardParameterType.Base, "BTC");
            baseTrans.IsLowercase = true;
            param.Add(baseTrans);

            ExchangeParameter counterTrans = new ExchangeParameter(ExchangeParameterType.Address, "counter", StandardParameterType.Counter, "USD");
            counterTrans.IsLowercase = true;
            param.Add(counterTrans);

            var transactions = new ExchangeCommand(CommandClass.Transactions,
                                                        Method.GET,
                                                        new Uri("/v1/trades/{0}{1}", UriKind.Relative),
                                                        false,
                                                        typeof(List<BitFinexTransactionJSON>),
                                                        param);

            transactions.LowercaseUrlParameters = true;

            return transactions;
        }

        public ExchangeCommand BuildUserTransactionsCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "symbol", StandardParameterType.Pair, "BTCUSD"));

            var userTransactions = new ExchangeCommand(CommandClass.UserTransactions,
                                                        Method.POST,
                                                        new Uri("/v1/mytrades", UriKind.Relative),
                                                        true,
                                                        typeof(List<BitFinexUserTransactionJSON>),
                                                        param);

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