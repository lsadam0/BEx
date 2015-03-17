using BEx.CommandProcessing;
using RestSharp;
using System.Collections.Generic;

namespace BEx.ExchangeSupport.BitStampSupport
{
    internal class BitStampCommandFactory : IExchangeCommandFactory
    {
        private Dictionary<CommandClass, ExchangeCommand> collection;

        public BitStampCommandFactory()
        {
            collection = GetCommandCollection();
        }

        public ExchangeCommand BuildAccountBalanceCommand()
        {
            var accountBalance = new ExchangeCommand(CommandClass.AccountBalance,
                                                     Method.POST,
                                                    "balance/",
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
                                                   "buy/",
                                                   true,
                                                   typeof(BitStampOrderConfirmationJSON),
                                                   false,
                                                   param);

            return buyOrder;
        }

        public ExchangeCommand BuildCancelOrderCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "id", StandardParameterType.Id));

            var cancelOrder = new ExchangeCommand(CommandClass.CancelOrder,
                                                                Method.POST,
                                                                "cancel_order/",
                                                                true,
                                                                typeof(Confirmation),
                                                                true,
                                                                param);

            return cancelOrder;
        }

        public ExchangeCommand BuildDepositAddressCommand()
        {
            var depositAddress = new ExchangeCommand(CommandClass.DepositAddress,
                                                      Method.POST,
                                                      "bitcoin_deposit_address/",
                                                      true,
                                                      typeof(string),
                                                      true);

            return depositAddress;
        }

        public ExchangeCommand BuildOpenOrdersCommand()
        {
            var openOrders = new ExchangeCommand(CommandClass.OpenOrders,
                                                    Method.POST,
                                                    "open_orders/",
                                                    true,
                                                    typeof(List<BitStampOpenOrdersJSON>)
                                                    );

            return openOrders;
        }

        public ExchangeCommand BuildOrderBookCommand()
        {
            var orderBook = new ExchangeCommand(CommandClass.OrderBook,
                                                                 Method.GET,
                                                                 "order_book/",
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
                                                    "sell/",
                                                    true,
                                                    typeof(BitStampOrderConfirmationJSON),
                                                    false,
                                                    param);

            return sellOrder;
        }

        public ExchangeCommand BuildTickCommand()
        {
            var tick = new ExchangeCommand(CommandClass.Tick,
                                                      Method.GET,
                                                     "ticker/",
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
                                                                "transactions/",
                                                                false,
                                                                typeof(List<BitstampTransactionJSON>),
                                                                false,
                                                                param);

            return transactions;
        }

        public ExchangeCommand BuildUserTransactionsCommand()
        {
            var userTransactions = new ExchangeCommand(CommandClass.UserTransactions,
                                                        Method.POST,
                                                        "user_transactions/",
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
    }
}