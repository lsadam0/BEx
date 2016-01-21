// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using NUnit.Framework;

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
            var toVerify = TestCandidate.GetAccountBalance();

            VerifyAPIResult(toVerify);

            CollectionAssert.IsNotEmpty(toVerify.BalanceByCurrency);

            foreach (var c in TestCandidate.SupportedCurrencies)
            {
                Assert.That(toVerify.BalanceByCurrency.ContainsKey(c));

                var individualBalance = toVerify.BalanceByCurrency[c];

                VerifyAPIResult(individualBalance);
                Assert.That(individualBalance.BalanceCurrency == c);
                Assert.That(individualBalance.AvailableToTrade >= 0);
                Assert.That(individualBalance.TotalBalance >= 0);
            }
        }

        public void VerifyAPIResult(IExchangeResult toVerify)
        {
            Assert.IsNotNull(toVerify);
            Assert.That(toVerify.SourceExchange == TestCandidate.ExchangeSourceType);

            Assert.That(toVerify.ExchangeTimeStampUTC > DateTime.MinValue);
            Assert.That(toVerify.ExchangeTimeStampUTC < DateTime.MaxValue);

            Assert.That(toVerify.LocalTimeStampUTC > DateTime.MinValue);
            Assert.That(toVerify.LocalTimeStampUTC < DateTime.MaxValue);
        }

        public void VerifyBuyOrder()
        {
            var toVerify = TestCandidate.CreateBuyLimitOrder(1m, 5m);

            VerifyAPIResult(toVerify);

            VerifyOrder(toVerify, TestCandidate.DefaultPair, OrderType.Buy);
        }

        public void VerifyDepositAddress(Currency depositCurrency)
        {
            var address = TestCandidate.GetDepositAddress(depositCurrency);

            VerifyAPIResult(address);

            Assert.That(!string.IsNullOrWhiteSpace(address.Address));
            Assert.That(address.DepositCurrency == depositCurrency);
        }

        public void VerifyOpenOrders()
        {
            var toVerify = TestCandidate.GetOpenOrders();

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

        public void VerifyOrderBook(TradingPair pair)
        {
            var toVerify = TestCandidate.GetOrderBook(pair);

            VerifyAPIResult(toVerify);

            Assert.That(toVerify.Pair == pair);

            CollectionAssert.IsNotEmpty(toVerify.Asks);
            CollectionAssert.AllItemsAreNotNull(toVerify.Asks);
            CollectionAssert.AllItemsAreInstancesOfType(toVerify.Asks, typeof (OrderBookEntry));
            CollectionAssert.AllItemsAreUnique(toVerify.Asks);

            CollectionAssert.IsNotEmpty(toVerify.Bids);
            CollectionAssert.AllItemsAreNotNull(toVerify.Bids);
            CollectionAssert.AllItemsAreInstancesOfType(toVerify.Bids, typeof (OrderBookEntry));
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
            var toVerify = TestCandidate.CreateSellLimitOrder(0.02m, 10000m);

            VerifyAPIResult(toVerify);

            VerifyOrder(toVerify, TestCandidate.DefaultPair, OrderType.Sell);
        }

        public void VerifyTick(TradingPair pair)
        {
            var toVerify = TestCandidate.GetTick(pair);

            VerifyAPIResult(toVerify);

            Assert.That(toVerify.Pair == pair);
            Assert.That(toVerify.Ask > 0.0m);
            Assert.That(toVerify.Bid > 0.0m);
            Assert.That(toVerify.Last > 0.0m);
            Assert.That(toVerify.High > 0.0m);
            Assert.That(toVerify.Low > 0.0m);
            Assert.That(toVerify.Volume > 0.0m);
        }

        public void VerifyTransactions(TradingPair pair)
        {
            var current = DateTime.UtcNow;
            var toVerify = TestCandidate.GetTransactions(pair);

            VerifyAPIResult(toVerify);

            Assert.IsNotNull(toVerify.TransactionsCollection);

            CollectionAssert.IsNotEmpty(toVerify.TransactionsCollection);
            CollectionAssert.AllItemsAreNotNull(toVerify.TransactionsCollection);
            CollectionAssert.AllItemsAreInstancesOfType(toVerify.TransactionsCollection, typeof (Transaction));
            CollectionAssert.AllItemsAreUnique(toVerify.TransactionsCollection);

            var ordered = toVerify.TransactionsCollection.OrderByDescending(x => x.CompletedTime);
            Assert.That(ordered.SequenceEqual(toVerify.TransactionsCollection));

            var minimumTime = toVerify.TransactionsCollection.Min(x => x.CompletedTime);
            Assert.That(minimumTime, Is.InRange(current.AddMinutes(-61), current));

            foreach (var t in toVerify.TransactionsCollection)
            {
                VerifyAPIResult(t);

                Assert.That(t.CompletedTime, Is.InRange(current.AddMinutes(-61), current));

                Assert.That(t.Pair == pair);
                Assert.That(t.Amount > 0.0m);

                Assert.That(t.Price > 0.0m);
                Assert.That(t.TransactionId > 0);
            }
        }

        public void VerifyUserTransactions(TradingPair pair)
        {
            var toVerify = TestCandidate.GetUserTransactions(pair);

            VerifyAPIResult(toVerify);

            Assert.IsNotNull(toVerify.TransactionsCollection);

            CollectionAssert.IsNotEmpty(toVerify.TransactionsCollection);
            CollectionAssert.AllItemsAreNotNull(toVerify.TransactionsCollection);
            CollectionAssert.AllItemsAreInstancesOfType(toVerify.TransactionsCollection, typeof (UserTransaction));
            CollectionAssert.AllItemsAreUnique(toVerify.TransactionsCollection);

            var ordered = toVerify.TransactionsCollection.OrderByDescending(x => x.CompletedTime);
            Assert.That(ordered.SequenceEqual(toVerify.TransactionsCollection));

            Assert.That(toVerify.TransactionsCollection.Count <= 50);

            foreach (var transaction in toVerify.TransactionsCollection)
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