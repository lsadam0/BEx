using BEx;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NUnitTests
{
    public class ExchangeCommandVerification : ExchangeVerificationBase
    {

        public ExchangeCommandVerification(Exchange testCandidate)
            : base(testCandidate)
        {

        }

        public void VerifyAPIResult(APIResult toVerify)
        {
            Assert.IsNotNull(toVerify);

            Assert.IsTrue(toVerify.ExchangeTimeStamp > DateTime.MinValue);
            Assert.IsTrue(toVerify.ExchangeTimeStamp < DateTime.MaxValue);

            Assert.IsTrue(toVerify.LocalTimeStamp > DateTime.MinValue);
            Assert.IsTrue(toVerify.LocalTimeStamp < DateTime.MaxValue);
        }

        public void VerifyTick()
        {
            foreach (KeyValuePair<Currency, HashSet<Currency>> pairSet in testCandidate.SupportedTradingPairs)
            {
                Currency baseCurrency = pairSet.Key;

                foreach (Currency counterCurrency in pairSet.Value)
                {
                    ThrottleTestVelocity();

                    Debug(string.Format("Verifying Tick for {0}/{1}", baseCurrency, counterCurrency));

                    Tick toVerify = testCandidate.GetTick(baseCurrency, counterCurrency);

                    VerifyAPIResult(toVerify);

                    Assert.IsTrue(toVerify.BaseCurrency == baseCurrency);
                    Assert.IsTrue(toVerify.CounterCurrency == counterCurrency);
                    Assert.IsTrue(toVerify.Ask > 0.0m);
                    Assert.IsTrue(toVerify.Bid > 0.0m);
                    Assert.IsTrue(toVerify.Last > 0.0m);
                    Assert.IsTrue(toVerify.High > 0.0m);
                    Assert.IsTrue(toVerify.Low > 0.0m);
                    Assert.IsTrue(toVerify.Volume > 0.0m);


                    Debug(toVerify.ToString());
                }
            }
        }

        public void VerifyOrderBook()
        {
            foreach (KeyValuePair<Currency, HashSet<Currency>> pairSet in testCandidate.SupportedTradingPairs)
            {
                Currency baseCurrency = pairSet.Key;

                foreach (Currency counterCurrency in pairSet.Value)
                {
                    ThrottleTestVelocity();

                    Debug(string.Format("Verifying OrderBook for {0}/{1}", baseCurrency, counterCurrency));

                    OrderBook toVerify = testCandidate.GetOrderBook(baseCurrency, counterCurrency);

                    VerifyAPIResult(toVerify);

                    Assert.IsTrue(toVerify.BaseCurrency == baseCurrency);
                    Assert.IsTrue(toVerify.CounterCurrency == counterCurrency);
                    Assert.IsTrue(toVerify.BidsByPrice.Keys.Count > 0);
                    Assert.IsTrue(toVerify.AsksByPrice.Keys.Count > 0);

                    foreach (KeyValuePair<decimal, decimal> order in toVerify.BidsByPrice)
                    {
                        Assert.IsTrue(order.Key >= 0.0m);
                        Assert.IsTrue(order.Value > 0.0m);
                    }

                    foreach (KeyValuePair<decimal, decimal> order in toVerify.AsksByPrice)
                    {
                        Assert.IsTrue(order.Key >= 0.0m);
                        Assert.IsTrue(order.Value > 0.0m);
                    }

                    Debug(toVerify.ToString());
                }
            }
        }

        public void VerifyTransactions()
        {
            foreach (KeyValuePair<Currency, HashSet<Currency>> pairSet in testCandidate.SupportedTradingPairs)
            {
                Currency baseCurrency = pairSet.Key;

                foreach (Currency counterCurrency in pairSet.Value)
                {
                    ThrottleTestVelocity();

                    Debug(string.Format("Verifying Transactions for {0}/{1}", baseCurrency, counterCurrency));

                    Transactions toVerify = testCandidate.GetTransactions(baseCurrency, counterCurrency);

                    VerifyAPIResult(toVerify);

                    if (baseCurrency == Currency.BTC && counterCurrency == Currency.USD)
                        Assert.IsTrue(toVerify.TransactionsCollection.Count > 0);

                    if (toVerify.TransactionsCollection.Count > 0)
                    {
                        foreach (Transaction t in toVerify.TransactionsCollection)
                        {
                            VerifyAPIResult(t);
                            Assert.IsTrue(t.Amount > 0.0m);
                            Assert.IsTrue(t.Price > 0.0m);
                            Assert.IsTrue(t.TransactionID > 0);
                        }

                        DateTime oldest = toVerify.TransactionsCollection.Min(x => x.CompletedTime);
                        DateTime newest = toVerify.TransactionsCollection.Max(x => x.CompletedTime);

                        int totalMinutes = (newest - oldest).Minutes;
                        Assert.IsTrue(totalMinutes < 61);
                    }

                    Debug(toVerify.ToString());
                }
            }
        }

        public void VerifyAccountBalance()
        {
            ThrottleTestVelocity();

            AccountBalance toVerify = testCandidate.GetAccountBalance();

            VerifyAPIResult(toVerify);

            Assert.IsTrue(toVerify.BalanceByCurrency.Count > 0);

            Assert.IsTrue(toVerify.BalanceByCurrency.ContainsKey(Currency.BTC));

            foreach (Currency c in testCandidate.SupportedCurrencies)
            {
                if (toVerify.BalanceByCurrency.ContainsKey(c))
                {
                    Balance individualBalance = toVerify.BalanceByCurrency[c];

                    Assert.IsTrue(individualBalance.AvailableToTrade >= 0);
                    Assert.IsTrue(individualBalance.TotalBalance >= 0);
                }
            }
        }

        public void VerifyBuyOrder()
        {
            ThrottleTestVelocity();

            Debug("Begin Buy Order Test");
            Order toVerify = testCandidate.CreateBuyOrder(1m, 5m);

            VerifyAPIResult(toVerify);

            Assert.IsTrue(toVerify.TradeType == OrderType.Buy);
            Assert.IsTrue(toVerify.Amount > 0.0m);
            Assert.IsTrue(toVerify.ID > 0);
            Assert.IsTrue(toVerify.Price > 0.0m);

            OpenOrders open = testCandidate.GetOpenOrders();

            Assert.IsTrue(open.Orders.Count > 0);

            Assert.IsNotNull(open.Orders[toVerify.ID]);//.Find(x => x.ID == toVerify.ID));

            Debug(string.Format("Cancelling Buy Order {0}", toVerify.ID));
            bool cancelled = testCandidate.CancelOrder(toVerify.ID);

            Assert.IsTrue(cancelled);

            Debug(string.Format("Cancelled Sell Order {0}", toVerify.ID));

            open = testCandidate.GetOpenOrders();

            Assert.IsTrue(open.Orders.Count == 0);
        }

        public void VerifySellOrder()
        {
            ThrottleTestVelocity();



            Debug("Begin Sell Order");
            Order toVerify = testCandidate.CreateSellOrder(0.02m, 10000m);

            VerifyAPIResult(toVerify);

            Assert.IsTrue(toVerify.TradeType == OrderType.Sell);
            Assert.IsTrue(toVerify.Amount > 0.0m);
            Assert.IsTrue(toVerify.ID > 0);
            Assert.IsTrue(toVerify.Price > 0.0m);

            OpenOrders open = testCandidate.GetOpenOrders();

            Assert.IsTrue(open.Orders.Count > 0);

            Assert.IsNotNull(open.Orders[toVerify.ID]);

            Debug(string.Format("Cancelling Sell Order {0}", toVerify.ID));
            bool cancelled = testCandidate.CancelOrder(toVerify.ID);

            Assert.IsTrue(cancelled);

            Debug(string.Format("Cancelled Sell Order {0}", toVerify.ID));

            open = testCandidate.GetOpenOrders();

            Debug("Checking Open Orders");
            Assert.IsTrue(open.Orders.Count == 0);

        }

        public void VerifyOpenOrders()
        {
            OpenOrders toVerify = testCandidate.GetOpenOrders();

            VerifyAPIResult(toVerify);

            Assert.IsTrue(toVerify.Orders.Count > 0);

            foreach (KeyValuePair<int, Order> ord in toVerify.Orders)
            {
                VerifyAPIResult(ord.Value);
                Assert.IsTrue(ord.Value.Amount > 0.0m);
                Assert.IsTrue(ord.Value.ID > 0);
                Assert.IsTrue(ord.Value.Price > 0.0m);
            }
        }

        public void VerifyUserTransactions()
        {
            UserTransactions toVerify = testCandidate.GetUserTransactions();

            VerifyAPIResult(toVerify);

            Assert.IsNotNull(toVerify.UserTrans);
            Assert.IsTrue(toVerify.UserTrans.Count > 0);

            foreach (UserTransaction transaction in toVerify.UserTrans)
            {
                VerifyAPIResult(transaction);
                // Assert.IsTrue(transaction.BaseCurrencyAmount > 0.0m);
                // Assert.IsTrue(transaction.CounterCurrencyAmount > 0.0m);
                // Assert.IsTrue(transaction.ExchangeRate > 0.0m);
                Assert.IsTrue(transaction.ID > 0);
                Assert.IsTrue(transaction.OrderID != null);
                Assert.IsTrue(transaction.OrderID > 0);
            }

            Debug(toVerify.ToString());
        }

        public void VerifyDepositAddress(Currency depositCurrency)
        {
            DepositAddress address = testCandidate.GetDepositAddress(depositCurrency);

            VerifyAPIResult(address);

         //   throw new AssertionException("Deposit Address allows request of Fiat currencies");


            Assert.IsTrue(!string.IsNullOrEmpty(address.Address));
            Assert.IsTrue(address.DepositCurrency == depositCurrency);
           
        }
    }
}
