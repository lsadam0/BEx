// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Services;

namespace BEx.UnitTests
{
    public class ExchangeCommandVerification : ExchangeVerificationBase
    {
        public ExchangeCommandVerification(Exchange testCandidate)
            : base(testCandidate)
        {
        }

        public void VerifyAccountBalance()
        {
            AccountBalance toVerify = TestCandidate.GetAccountBalance();

            VerifyAPIResult(toVerify);

            CollectionAssert.IsNotEmpty(toVerify.BalanceByCurrency);


            foreach (Currency c in TestCandidate.SupportedCurrencies)
            {
                Assert.That(toVerify.BalanceByCurrency.ContainsKey(c));

                Balance individualBalance = toVerify.BalanceByCurrency[c];

                VerifyAPIResult(individualBalance);
                Assert.That(individualBalance.BalanceCurrency == c);
                Assert.That(individualBalance.AvailableToTrade >= 0);
                Assert.That(individualBalance.TotalBalance >= 0);

            }
        }

        public void VerifyAPIResult(ApiResult toVerify)
        {
            Assert.IsNotNull(toVerify);
            Assert.That(toVerify.SourceExchange == base.TestCandidate.ExchangeSourceType);

            Assert.That(toVerify.ExchangeTimestamp > DateTime.MinValue);
            Assert.That(toVerify.ExchangeTimestamp < DateTime.MaxValue);

            Assert.That(toVerify.LocalTimestamp > DateTime.MinValue);
            Assert.That(toVerify.LocalTimestamp < DateTime.MaxValue);
        }

        public void VerifyBuyOrder()
        {
            Order toVerify = TestCandidate.CreateBuyOrder(1m, 5m);

            VerifyAPIResult(toVerify);

            VerifyOrder(toVerify, TestCandidate.DefaultPair, OrderType.Buy);
        }

        public void VerifyDepositAddress(Currency depositCurrency)
        {
            DepositAddress address = TestCandidate.GetDepositAddress(depositCurrency);

            VerifyAPIResult(address);

            Assert.That(!string.IsNullOrWhiteSpace(address.Address));
            Assert.That(address.DepositCurrency == depositCurrency);
        }

        public void VerifyOpenOrders()
        {
            OpenOrders toVerify = TestCandidate.GetOpenOrders();

            VerifyAPIResult(toVerify);

            foreach (var openOrder in toVerify.BuyOrders)
            {
                var order = openOrder.Value;

                VerifyAPIResult(order);

                Assert.That(order.IsBuyOrder);
                Assert.That(order.Amount > 0.0m);
                Assert.That(order.Id > 0);
                Assert.That(openOrder.Key == order.Id);
                Assert.That(order.Price > 0.0m);

            }

            foreach (var openOrder in toVerify.SellOrders)
            {
                var order = openOrder.Value;

                VerifyAPIResult(order);

                Assert.That(order.IsSellOrder);
                Assert.That(order.Amount > 0.0m);
                Assert.That(order.Id > 0);
                Assert.That(openOrder.Key == order.Id);
                Assert.That(order.Price > 0.0m);
            }
        }

        public void VerifyOrderBook(CurrencyTradingPair pair)
        {
            OrderBook toVerify = TestCandidate.GetOrderBook(pair);

            VerifyAPIResult(toVerify);

            Assert.That(toVerify.Pair == pair);

            CollectionAssert.IsNotEmpty(toVerify.Asks);
            CollectionAssert.AllItemsAreNotNull(toVerify.Asks);
            CollectionAssert.AllItemsAreInstancesOfType(toVerify.Asks, typeof(OrderBookEntry));
            CollectionAssert.AllItemsAreUnique(toVerify.Asks);

            CollectionAssert.IsNotEmpty(toVerify.Bids);
            CollectionAssert.AllItemsAreNotNull(toVerify.Bids);
            CollectionAssert.AllItemsAreInstancesOfType(toVerify.Bids, typeof(OrderBookEntry));
            CollectionAssert.AllItemsAreUnique(toVerify.Bids);

            var orderedAsks = toVerify.Asks.OrderBy(x => x.Price);
            Assert.That(orderedAsks.SequenceEqual(toVerify.Asks));

            var orderedBids = toVerify.Bids.OrderByDescending(x => x.Price);
            Assert.That(orderedBids.SequenceEqual(toVerify.Bids));

            foreach (var entry in toVerify.Asks)
            {
                Assert.That(entry.Amount > 0.0m);
                Assert.That(entry.Price > 0.0m);

            }

            foreach (var entry in toVerify.Bids)
            {
                Assert.That(entry.Amount > 0.0m);
                Assert.That(entry.Price > 0.0m);
            }
        }

        public void VerifySellOrder()
        {
            Order toVerify = TestCandidate.CreateSellOrder(0.02m, 10000m);

            VerifyAPIResult(toVerify);

            VerifyOrder(toVerify, TestCandidate.DefaultPair, OrderType.Sell);
        }

        public void VerifyTick(CurrencyTradingPair pair)
        {
            Tick toVerify = TestCandidate.GetTick(pair);

            VerifyAPIResult(toVerify);

            Assert.That(toVerify.Pair == pair);
            Assert.That(toVerify.Ask > 0.0m);
            Assert.That(toVerify.Bid > 0.0m);
            Assert.That(toVerify.Last > 0.0m);
            Assert.That(toVerify.High > 0.0m);
            Assert.That(toVerify.Low > 0.0m);
            Assert.That(toVerify.Volume > 0.0m);

        }

        public void VerifyTransactions(CurrencyTradingPair pair)
        {
            DateTime current = DateTime.UtcNow;
            Transactions toVerify = TestCandidate.GetTransactions(pair);

            VerifyAPIResult(toVerify);

            Assert.IsNotNull(toVerify.TransactionsCollection);

            CollectionAssert.IsNotEmpty(toVerify.TransactionsCollection);
            CollectionAssert.AllItemsAreNotNull(toVerify.TransactionsCollection);
            CollectionAssert.AllItemsAreInstancesOfType(toVerify.TransactionsCollection, typeof(BEx.Transaction));
            CollectionAssert.AllItemsAreUnique(toVerify.TransactionsCollection);

            var ordered = toVerify.TransactionsCollection.OrderByDescending(x => x.CompletedTime);
            Assert.That(ordered.SequenceEqual(toVerify.TransactionsCollection));

            DateTime minimumTime = toVerify.TransactionsCollection.Min(x => x.CompletedTime);
            Assert.That(minimumTime, Is.InRange(current.AddMinutes(-61), current));

            foreach (Transaction t in toVerify.TransactionsCollection)
            {
                VerifyAPIResult(t);

                Assert.That(t.CompletedTime, Is.InRange(current.AddMinutes(-61), current));

                Assert.That(t.Pair == pair);
                Assert.That(t.Amount > 0.0m);

                Assert.That(t.Price > 0.0m);
                Assert.That(t.TransactionId > 0);
            }
        }

        public void VerifyUserTransactions(CurrencyTradingPair pair)
        {
            UserTransactions toVerify = TestCandidate.GetUserTransactions(pair);

            VerifyAPIResult(toVerify);

            Assert.IsNotNull(toVerify.TransactionsCollection);

            CollectionAssert.IsNotEmpty(toVerify.TransactionsCollection);
            CollectionAssert.AllItemsAreNotNull(toVerify.TransactionsCollection);
            CollectionAssert.AllItemsAreInstancesOfType(toVerify.TransactionsCollection, typeof(BEx.UserTransaction));
            CollectionAssert.AllItemsAreUnique(toVerify.TransactionsCollection);

            var ordered = toVerify.TransactionsCollection.OrderByDescending(x => x.CompletedTime);
            Assert.That(ordered.SequenceEqual(toVerify.TransactionsCollection));

            Assert.That(toVerify.TransactionsCollection.Count <= 50);

            foreach (UserTransaction transaction in toVerify.TransactionsCollection)
            {
                VerifyAPIResult(transaction);

                Assert.That(transaction.Pair == pair);

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

                Assert.That(Math.Round(((transaction.BaseCurrencyAmount * transaction.ExchangeRate) +
                               transaction.CounterCurrencyAmount), 2) == 0);


                // Trade Fee Currency belongs to Trading Pair
                Assert.That((transaction.Pair.BaseCurrency == transaction.TradeFeeCurrency)
                                ||
                                (transaction.Pair.CounterCurrency == transaction.TradeFeeCurrency));


                Assert.That(transaction.OrderId > 0);
            }
        }

        private void VerifyOrder(Order toVerify, CurrencyTradingPair pair, OrderType requestedOrderType)
        {
            VerifyAPIResult(toVerify);

            Assert.That(toVerify.Amount > 0);
            Assert.That(toVerify.Id > 0);
            Assert.That(toVerify.Price > 0);
            Assert.That(toVerify.SourceExchange == TestCandidate.ExchangeSourceType);
            Assert.That(toVerify.Pair.BaseCurrency == pair.BaseCurrency);
            Assert.That(toVerify.Pair.CounterCurrency == pair.CounterCurrency);
            Assert.That(toVerify.TradeType == requestedOrderType);
        }
    }
}