// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using BEx.ExchangeEngine;
using NUnit.Framework;

namespace BEx.Tests
{
    public class ExchangeCommandVerification
    {
        public ExchangeCommandVerification(Exchange testCandidate)
        {
            TestCandidate = testCandidate;
        }

        protected Exchange TestCandidate { get; set; }

        public void VerifyAccountBalance()
        {
            var toVerify = TestCandidate.GetAccountBalance();

            VerifyApiResult(toVerify);

            CollectionAssert.IsNotEmpty(toVerify.BalanceByCurrency);

            foreach (var c in TestCandidate.SupportedCurrencies)
            {
                Assert.That(toVerify.BalanceByCurrency.ContainsKey(c));

                var individualBalance = toVerify.BalanceByCurrency[c];

                VerifyApiResult(individualBalance);

                Assert.That(individualBalance.BalanceCurrency == c);
                Assert.That(individualBalance.AvailableToTrade >= 0);
                Assert.That(individualBalance.TotalBalance >= 0);
                Assert.That(individualBalance.AvailableToTrade + individualBalance.Reserved ==
                            individualBalance.TotalBalance);
            }
        }

        public void VerifyApiResult(IExchangeResult toVerify)
        {
            Assert.IsNotNull(toVerify);

            Assert.That(toVerify.SourceExchange == TestCandidate.ExchangeSourceType);

            Assert.That(toVerify.ExchangeTimeStampUTC.Kind == DateTimeKind.Utc);
            Assert.That(toVerify.ExchangeTimeStampUTC != default(DateTime));
            Assert.That(toVerify.ExchangeTimeStampUTC > DateTime.MinValue);
            Assert.That(toVerify.ExchangeTimeStampUTC < DateTime.MaxValue);

            Assert.That(toVerify.LocalTimeStampUTC != default(DateTime));
            Assert.That(toVerify.LocalTimeStampUTC > DateTime.MinValue);
            Assert.That(toVerify.LocalTimeStampUTC < DateTime.MaxValue);
            Assert.That(toVerify.LocalTimeStampUTC.Kind == DateTimeKind.Utc);
        }

        public void VerifyBuyOrder()
        {
            var toVerify = TestCandidate.CreateBuyLimitOrder(1m, 5m);

            VerifyApiResult(toVerify);

            VerifyOrder(toVerify, TestCandidate.DefaultPair, OrderType.Buy);
        }

        public void VerifyDepositAddress(Currency depositCurrency)
        {
            var toTest = default(DepositAddress);
            Assert.DoesNotThrow(() => { toTest = TestCandidate.GetDepositAddress(depositCurrency); });

            VerifyApiResult(toTest);

            Assert.That(!string.IsNullOrWhiteSpace(toTest.Address));
            Assert.That(!string.IsNullOrEmpty(toTest.Address));
            Assert.That(toTest.DepositCurrency == depositCurrency,
                $"DepositAddress Currency mismatch.  Request: {depositCurrency} Response: {toTest.DepositCurrency}");
        }

        public void VerifyOpenOrders()
        {
            var toVerify = TestCandidate.GetOpenOrders();

            VerifyApiResult(toVerify);

            foreach (var openOrder in toVerify.BuyOrders)
            {
                var order = openOrder.Value;

                VerifyApiResult(order);

                Assert.That(order.IsBuyOrder);
                Assert.That(order.Amount > 0.0m);
                Assert.That(!string.IsNullOrEmpty(order.Id));
                Assert.That(openOrder.Key == order.Id);
                Assert.That(order.Price > 0.0m);
            }

            foreach (var openOrder in toVerify.SellOrders)
            {
                var order = openOrder.Value;

                VerifyApiResult(order);

                Assert.That(order.IsSellOrder);
                Assert.That(order.Amount > 0.0m);
                Assert.That(!string.IsNullOrEmpty(order.Id));
                Assert.That(openOrder.Key == order.Id);
                Assert.That(order.Price > 0.0m);
            }
        }

        public void VerifyOrderBook(TradingPair pair)
        {
            var toVerify = TestCandidate.GetOrderBook(pair);

            VerifyApiResult(toVerify);

            Assert.That(toVerify.Pair == pair);

            CollectionAssert.IsNotEmpty(toVerify.Asks);
            CollectionAssert.AllItemsAreNotNull(toVerify.Asks);
            CollectionAssert.AllItemsAreInstancesOfType(toVerify.Asks, typeof (OrderBookEntry));
            CollectionAssert.AllItemsAreUnique(toVerify.Asks);
            CollectionAssert.DoesNotContain(toVerify.Asks, default(OrderBookEntry));

            CollectionAssert.IsNotEmpty(toVerify.Bids);
            CollectionAssert.AllItemsAreNotNull(toVerify.Bids);
            CollectionAssert.AllItemsAreInstancesOfType(toVerify.Bids, typeof (OrderBookEntry));
            CollectionAssert.AllItemsAreUnique(toVerify.Bids);
            CollectionAssert.DoesNotContain(toVerify.Bids, default(OrderBookEntry));

            CollectionAssert.AreNotEqual(toVerify.Asks, toVerify.Bids);

            var orderedAsks = toVerify.Asks.OrderBy(x => x.Price);
            Assert.That(orderedAsks.SequenceEqual(toVerify.Asks));

            var orderedBids = toVerify.Bids.OrderByDescending(x => x.Price);
            Assert.That(orderedBids.SequenceEqual(toVerify.Bids));

            foreach (var entry in toVerify.Asks)
            {
                VerifyApiResult(entry);
                Assert.That(entry.Amount > 0.0m);
                Assert.That(entry.Price > 0.0m);
                Assert.That(entry.UnixTimeStamp > 0);
            }

            foreach (var entry in toVerify.Bids)
            {
                VerifyApiResult(entry);
                Assert.That(entry.Amount > 0.0m);
                Assert.That(entry.Price > 0.0m);
                Assert.That(entry.UnixTimeStamp > 0);
            }
        }

        public void VerifySellOrder()
        {
            var toVerify = TestCandidate.CreateSellLimitOrder(0.02m, 10000m);

            VerifyApiResult(toVerify);

            VerifyOrder(toVerify, TestCandidate.DefaultPair, OrderType.Sell);
        }

        public void VerifyTick(TradingPair pair)
        {
            var toVerify = TestCandidate.GetTick(pair);

            VerifyApiResult(toVerify);

            Assert.That(toVerify.Pair == pair);
            Assert.That(toVerify.Ask > 0.0m);
            Assert.That(toVerify.Bid > 0.0m);
            Assert.That(toVerify.Last > 0.0m);
            Assert.That(toVerify.Volume > 0.0m);
            Assert.That(toVerify.UnixTimeStamp > 0);
        }

        public void VerifyTransactions(TradingPair pair)
        {
            var toVerify = TestCandidate.GetTransactions(pair);

            var previousHour = DateTime.UtcNow.AddMinutes(-61);
            var current = DateTime.UtcNow;

            VerifyApiResult(toVerify);

            Assert.IsNotNull(toVerify.TransactionsCollection);

            CollectionAssert.IsNotEmpty(toVerify.TransactionsCollection);
            CollectionAssert.DoesNotContain(toVerify.TransactionsCollection, default(Transaction));
            CollectionAssert.AllItemsAreInstancesOfType(toVerify.TransactionsCollection, typeof (Transaction));
            CollectionAssert.AllItemsAreUnique(toVerify.TransactionsCollection);
            CollectionAssert.AreEqual(toVerify.TransactionsCollection,
                toVerify.TransactionsCollection.OrderByDescending(x => x.UnixCompletedTimeStamp));

            var minimumTime = toVerify.TransactionsCollection.Min(x => x.CompletedTime);
            Assert.That(minimumTime, Is.InRange(previousHour, current));

            foreach (var transaction in toVerify.TransactionsCollection)
            {
                VerifyApiResult(transaction);

                Assert.That(transaction.UnixCompletedTimeStamp > 0);
                Assert.That(transaction.CompletedTime, Is.InRange(previousHour, current));

                Assert.That(transaction.Pair == pair);
                Assert.That(transaction.Amount > 0.0m);

                Assert.That(transaction.Price > 0.0m);
                Assert.That(transaction.TransactionId > 0);
            }
        }

        public void VerifyUserTransactions(TradingPair pair)
        {
            var toVerify = TestCandidate.GetUserTransactions(pair);

            VerifyApiResult(toVerify);

            Assert.IsNotNull(toVerify.TransactionsCollection);

            CollectionAssert.IsNotEmpty(toVerify.TransactionsCollection);
            CollectionAssert.AllItemsAreNotNull(toVerify.TransactionsCollection);
            CollectionAssert.AllItemsAreInstancesOfType(toVerify.TransactionsCollection, typeof (UserTransaction));
            CollectionAssert.AllItemsAreUnique(toVerify.TransactionsCollection);
            CollectionAssert.DoesNotContain(toVerify.TransactionsCollection, default(UserTransaction));
            CollectionAssert.AreEqual(toVerify.TransactionsCollection,
                new List<UserTransaction>(toVerify.TransactionsCollection).OrderByDescending(x => x.UnixTimeStamp));

            foreach (var transaction in toVerify.TransactionsCollection)
            {
                VerifyApiResult(transaction);

                Assert.That(transaction.Pair == pair);

                Assert.That(transaction.CompletedTime.Kind == DateTimeKind.Utc);

                // Check that correct sign is used
                if (transaction.TransactionType == OrderType.Sell)
                {
                    Assert.Less(transaction.BaseCurrencyAmount, 0);
                    Assert.Greater(transaction.CounterCurrencyAmount, 0);
                }
                else
                {
                    Assert.Greater(transaction.BaseCurrencyAmount, 0);
                    Assert.Less(transaction.CounterCurrencyAmount, 0);
                }

                Assert.Greater(transaction.ExchangeRate, 0);

                // Check that transaction balances

                Assert.That(Math.Round(transaction.BaseCurrencyAmount*transaction.ExchangeRate +
                                       transaction.CounterCurrencyAmount, 2) == 0);

                // Trade Fee Currency belongs to Trading Pair
                Assert.That((transaction.Pair.BaseCurrency == transaction.TradeFeeCurrency)
                            ||
                            (transaction.Pair.CounterCurrency == transaction.TradeFeeCurrency));

                Assert.That(transaction.OrderId > 0);
            }
        }

        private void VerifyOrder(Order toVerify, TradingPair pair, OrderType requestedOrderType)
        {
            VerifyApiResult(toVerify);

            Assert.That(toVerify.Amount > 0);
            Assert.That(!string.IsNullOrEmpty(toVerify.Id));
            Assert.That(toVerify.Price > 0);
            Assert.That(toVerify.SourceExchange == TestCandidate.ExchangeSourceType);
            Assert.That(toVerify.Pair.BaseCurrency == pair.BaseCurrency);
            Assert.That(toVerify.Pair.CounterCurrency == pair.CounterCurrency);
            Assert.That(toVerify.TradeType == requestedOrderType);
        }

        public void VerifyDayRange(TradingPair pair)
        {
            var toVerify = TestCandidate.Get24HrStats(pair);

            VerifyApiResult(toVerify);

            Assert.That(toVerify.High > 0.0m);
            Assert.That(toVerify.Low > 0.0m);
        }
    }
}