using BEx.Request;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEx.BitFinexSupport
{
    public class BitFinexCommandFactory : IExchangeCommandFactory
    {
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

        public ExchangeCommand BuildAccountBalanceCommand()
        {
            var accountBalance = new ExchangeCommand(CommandClass.AccountBalance,
                                                        Method.POST,
                                                        "/v1/balances",
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
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "is_hidden", StandardParameterType.None, "false"));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "side", StandardParameterType.None, "buy"));

            var buyOrder = new ExchangeCommand(CommandClass.BuyOrder,
                                                Method.POST,
                                                "/v1/order/new",
                                                true,
                                                typeof(BitFinexOrderResponseJSON),
                                                false,
                                                param);

            return buyOrder;
        }

        public ExchangeCommand BuildCancelOrderCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "order_id", StandardParameterType.Id));

            var cancelOrder = new ExchangeCommand(CommandClass.CancelOrder,
                                                                Method.POST,
                                                                "/v1/order/cancel",
                                                                true,
                                                                typeof(bool),
                                                                false,
                                                                param);

            return cancelOrder;
        }

        public ExchangeCommand BuildDepositAddressCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "currency", StandardParameterType.Currency, "BTC"));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "method", StandardParameterType.CurrencyFullName, "bitcoin"));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "wallet_name", StandardParameterType.None, "exchange"));

            var depositAddress = new ExchangeCommand(CommandClass.DepositAddress,
                                                         Method.POST,
                                                         "/v1/deposit/new",
                                                         true,
                                                         typeof(BitFinexDepositAddressJSON),
                                                         false,
                                                         param);

            return depositAddress;
        }

        public ExchangeCommand BuildOpenOrdersCommand()
        {
            var openOrders = new ExchangeCommand(CommandClass.OpenOrders,
                                                  Method.POST,
                                                  "/v1/orders",
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
                                                 "/v1/book/{0}{1}",
                                                 false,
                                                 typeof(BitFinexOrderBookJSON),
                                                 false,
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
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "is_hidden", StandardParameterType.None, "false"));
            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "side", StandardParameterType.None, "sell"));

            var sell = new ExchangeCommand(CommandClass.SellOrder,
                                               Method.POST,
                                               "/v1/order/new",
                                               true,
                                               typeof(BitFinexOrderResponseJSON),
                                               false,
                                               param);

            return sell;
        }

        public ExchangeCommand BuildTickCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Address, "pair", StandardParameterType.None));

            var tick = new ExchangeCommand(CommandClass.Tick,
                                            Method.GET,
                                            "/v1/pubticker/{0}{1}",
                                            false,
                                            typeof(BitfinexTickJSON),
                                            false,
                                            param);
            return tick;
        }

        public ExchangeCommand BuildTransactionsCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "timestamp", StandardParameterType.UnixTimeStamp, "needtoset"));

            ExchangeParameter baseTrans = new ExchangeParameter(ExchangeParameterType.Address, "base", StandardParameterType.Base, "BTC");
            baseTrans.IsLowerCase = true;
            param.Add(baseTrans);

            ExchangeParameter counterTrans = new ExchangeParameter(ExchangeParameterType.Address, "counter", StandardParameterType.Counter, "USD");
            counterTrans.IsLowerCase = true;
            param.Add(counterTrans);

            var transactions = new ExchangeCommand(CommandClass.Transactions,
                                                        Method.GET,
                                                        "/v1/trades/{0}{1}",
                                                        false,
                                                        typeof(List<BitFinexTransactionJSON>),
                                                        false,
                                                        param);

            transactions.LowerCaseURLParams = true;

            return transactions;
        }

        public ExchangeCommand BuildUserTransactionsCommand()
        {
            var param = new List<ExchangeParameter>();

            param.Add(new ExchangeParameter(ExchangeParameterType.Post, "symbol", StandardParameterType.Pair, "BTCUSD"));

            var userTransactions = new ExchangeCommand(CommandClass.UserTransactions,
                                                        Method.POST,
                                                        "/v1/mytrades",
                                                        true,
                                                        typeof(List<BitFinexUserTransactionJSON>),
                                                        false,
                                                        param);

            return userTransactions;
        }
    }
}