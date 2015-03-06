using BEx;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTests
{
    public class ExchangeCommandVerification : ExchangeVerificationBase
    {
        public ExchangeCommandVerification(Exchange testCandidate)
            : base(testCandidate)
        {
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

        public void VerifyAPIResult(APIResult toVerify)
        {
            Assert.IsNotNull(toVerify);
            Assert.IsTrue(toVerify.SourceExchange == base.testCandidate.ExchangeSourceType);

            Assert.IsTrue(toVerify.ExchangeTimeStamp > DateTime.MinValue);
            Assert.IsTrue(toVerify.ExchangeTimeStamp < DateTime.MaxValue);

            Assert.IsTrue(toVerify.LocalTimeStamp > DateTime.MinValue);
            Assert.IsTrue(toVerify.LocalTimeStamp < DateTime.MaxValue);
        }

        public void VerifyBuyOrder()
        {
            ThrottleTestVelocity();

            Debug("Begin Buy Order Test");
            Order toVerify = testCandidate.CreateBuyOrder(1m, 5m);

            VerifyAPIResult(toVerify);

            VerifyOrder(toVerify, testCandidate.DefaultPair, OrderType.Buy);
        }

        public void VerifyDepositAddress(Currency depositCurrency)
        {
            DepositAddress address = testCandidate.GetDepositAddress(depositCurrency);

            VerifyAPIResult(address);

            Assert.IsTrue(!string.IsNullOrEmpty(address.Address));
            Assert.IsTrue(address.DepositCurrency == depositCurrency);
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

        public void VerifyOrderBook()
        {
            // foreach (KeyValuePair<Currency, HashSet<Currency>> pairSet in testCandidate.SupportedTradingPairs)
            foreach (CurrencyTradingPair pair in testCandidate.SupportedTradingPairs)
            {
                {
                    ThrottleTestVelocity();

                    Debug(string.Format("Verifying OrderBook for {0}/{1}", pair.BaseCurrency, pair.CounterCurrency));

                    OrderBook toVerify = testCandidate.GetOrderBook(pair);

                    VerifyAPIResult(toVerify);

                    Assert.IsTrue(toVerify.Pair.BaseCurrency == pair.BaseCurrency);
                    Assert.IsTrue(toVerify.Pair.CounterCurrency == pair.CounterCurrency);
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

        public void VerifySellOrder()
        {
            ThrottleTestVelocity();

            Debug("Begin Sell Order");
            Order toVerify = testCandidate.CreateSellOrder(0.02m, 10000m);

            VerifyAPIResult(toVerify);

            VerifyOrder(toVerify, testCandidate.DefaultPair, OrderType.Sell);
        }

        public void VerifyTick()
        {
            foreach (CurrencyTradingPair pair in testCandidate.SupportedTradingPairs)
            {
                {
                    ThrottleTestVelocity();

                    Debug(string.Format("Verifying Tick for {0}/{1}", pair.BaseCurrency, pair.CounterCurrency));

                    Tick toVerify = testCandidate.GetTick(pair);

                    VerifyAPIResult(toVerify);

                    Assert.IsTrue(toVerify.Pair.BaseCurrency == pair.BaseCurrency);
                    Assert.IsTrue(toVerify.Pair.CounterCurrency == pair.CounterCurrency);
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

        public void VerifyTransactions()
        {
            foreach (CurrencyTradingPair pair in testCandidate.SupportedTradingPairs)
            {
                ThrottleTestVelocity();

                Debug(string.Format("Verifying Transactions for {0}/{1}", pair.BaseCurrency, pair.CounterCurrency));

                Transactions toVerify = testCandidate.GetTransactions(pair);

                VerifyAPIResult(toVerify);

                if (pair.BaseCurrency == Currency.BTC && pair.CounterCurrency == Currency.USD)
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

        public void VerifyUserTransactions()
        {
            UserTransactions toVerify = testCandidate.GetUserTransactions();

            VerifyAPIResult(toVerify);

            Assert.IsNotNull(toVerify.TransactionsCollection);
            Assert.IsTrue(toVerify.TransactionsCollection.Count > 0);

            foreach (UserTransaction transaction in toVerify.TransactionsCollection)
            {
                VerifyAPIResult(transaction);
                // Assert.IsTrue(transaction.BaseCurrencyAmount > 0.0m);
                // Assert.IsTrue(transaction.CounterCurrencyAmount > 0.0m);
                // Assert.IsTrue(transaction.ExchangeRate > 0.0m);
                Assert.IsTrue(transaction.TransactionId > 0);
                Assert.IsTrue(transaction.OrderId > 0);
            }

            Debug(toVerify.ToString());
        }

        private void VerifyOrder(Order toVerify, CurrencyTradingPair pair, OrderType requestedOrderType)
        {
            VerifyAPIResult(toVerify);

            Assert.IsTrue(toVerify.Amount > 0);
            Assert.IsTrue(toVerify.ID > 0);
            Assert.IsTrue(toVerify.Price > 0);
            Assert.IsTrue(toVerify.SourceExchange == testCandidate.ExchangeSourceType);
            Assert.IsTrue(toVerify.Pair.BaseCurrency == pair.BaseCurrency);
            Assert.IsTrue(toVerify.Pair.CounterCurrency == pair.CounterCurrency);
            Assert.IsTrue(toVerify.TradeType == requestedOrderType);
        }
    }
}