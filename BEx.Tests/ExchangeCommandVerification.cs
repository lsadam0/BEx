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

            Assert.IsTrue(toVerify.BalanceByCurrency.Count > 0);

            foreach (Currency c in TestCandidate.SupportedCurrencies)
            {
                Assert.IsTrue(toVerify.BalanceByCurrency.ContainsKey(c));

                Balance individualBalance = toVerify.BalanceByCurrency[c];

                VerifyAPIResult(individualBalance);
                Assert.IsTrue(individualBalance.BalanceCurrency == c);
                Assert.IsTrue(individualBalance.AvailableToTrade >= 0);
                Assert.IsTrue(individualBalance.TotalBalance >= 0);

            }
        }

        public void VerifyAPIResult(ApiResult toVerify)
        {
            Assert.IsNotNull(toVerify);
            Assert.IsTrue(toVerify.SourceExchange == base.TestCandidate.ExchangeSourceType);

            Assert.IsTrue(toVerify.ExchangeTimestamp > DateTime.MinValue);
            Assert.IsTrue(toVerify.ExchangeTimestamp < DateTime.MaxValue);

            Assert.IsTrue(toVerify.LocalTimestamp > DateTime.MinValue);
            Assert.IsTrue(toVerify.LocalTimestamp < DateTime.MaxValue);
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


            Assert.IsTrue(!string.IsNullOrEmpty(address.Address));
            Assert.IsTrue(address.DepositCurrency == depositCurrency);
        }

        public void VerifyOpenOrders()
        {
            OpenOrders toVerify = TestCandidate.GetOpenOrders();

            VerifyAPIResult(toVerify);

            foreach (KeyValuePair<int, Order> ord in toVerify.BuyOrders)
            {
                VerifyAPIResult(ord.Value);
                Assert.IsTrue(ord.Value.Amount > 0.0m);
                Assert.IsTrue(ord.Value.Id > 0);
                Assert.IsTrue(ord.Value.Price > 0.0m);
            }

            foreach (KeyValuePair<int, Order> ord in toVerify.SellOrders)
            {
                VerifyAPIResult(ord.Value);
                Assert.IsTrue(ord.Value.Amount > 0.0m);
                Assert.IsTrue(ord.Value.Id > 0);
                Assert.IsTrue(ord.Value.Price > 0.0m);
            }
        }

        public void VerifyOrderBook(CurrencyTradingPair pair)
        {
            OrderBook toVerify = TestCandidate.GetOrderBook(pair);

            VerifyAPIResult(toVerify);

            Assert.IsTrue(toVerify.Pair.BaseCurrency == pair.BaseCurrency);
            Assert.IsTrue(toVerify.Pair.CounterCurrency == pair.CounterCurrency);
            Assert.IsTrue(toVerify.Asks.Count > 0);
            Assert.IsTrue(toVerify.Bids.Count > 0);

            foreach (var entry in toVerify.Asks)
            {
                Assert.IsTrue(entry.Amount > 0.0m);
                Assert.IsTrue(entry.Price >= 0.0m);
            }

            foreach (var entry in toVerify.Bids)
            {
                Assert.IsTrue(entry.Amount > 0.0m);
                Assert.IsTrue(entry.Price >= 0.0m);
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

            Assert.IsTrue(toVerify.Pair.BaseCurrency == pair.BaseCurrency);
            Assert.IsTrue(toVerify.Pair.CounterCurrency == pair.CounterCurrency);
            Assert.IsTrue(toVerify.Ask > 0.0m);
            Assert.IsTrue(toVerify.Bid > 0.0m);
            Assert.IsTrue(toVerify.Last > 0.0m);
            Assert.IsTrue(toVerify.High > 0.0m);
            Assert.IsTrue(toVerify.Low > 0.0m);
            Assert.IsTrue(toVerify.Volume > 0.0m);

        }

        public void VerifyTransactions(CurrencyTradingPair pair)
        {

            Transactions toVerify = TestCandidate.GetTransactions(pair);

            VerifyAPIResult(toVerify);

            if (pair.BaseCurrency == Currency.BTC && pair.CounterCurrency == Currency.USD)
                Assert.IsTrue(toVerify.TransactionsCollection.Any());

            if (toVerify.TransactionsCollection.Any())
            {
                foreach (Transaction t in toVerify.TransactionsCollection)
                {
                    VerifyAPIResult(t);
                    Assert.IsTrue(t.Amount > 0.0m);
                    Assert.IsTrue(t.Price > 0.0m);
                    Assert.IsTrue(t.TransactionId > 0);
                }

                DateTime oldest = toVerify.TransactionsCollection.Min(x => x.CompletedTime);
                DateTime newest = toVerify.TransactionsCollection.Max(x => x.CompletedTime);

                int totalMinutes = (newest - oldest).Minutes;
                Assert.IsTrue(totalMinutes < 61);
            }
        }

        public void VerifyUserTransactions(CurrencyTradingPair pair)
        {

            UserTransactions toVerify = TestCandidate.GetUserTransactions(pair);

            VerifyAPIResult(toVerify);

            Assert.IsNotNull(toVerify.TransactionsCollection);

            if (pair == TestCandidate.DefaultPair)
                Assert.IsTrue(toVerify.TransactionsCollection.Any());

            foreach (UserTransaction transaction in toVerify.TransactionsCollection)
            {
                VerifyAPIResult(transaction);
                // Assert.IsTrue(transaction.BaseCurrencyAmount > 0.0m);
                // Assert.IsTrue(transaction.CounterCurrencyAmount > 0.0m);
                // Assert.IsTrue(transaction.ExchangeRate > 0.0m);
                Assert.IsTrue(transaction.TransactionId > 0);
                Assert.IsTrue(transaction.OrderId > 0);
            }
        }

        private void VerifyOrder(Order toVerify, CurrencyTradingPair pair, OrderType requestedOrderType)
        {
            VerifyAPIResult(toVerify);

            Assert.IsTrue(toVerify.Amount > 0);
            Assert.IsTrue(toVerify.Id > 0);
            Assert.IsTrue(toVerify.Price > 0);
            Assert.IsTrue(toVerify.SourceExchange == TestCandidate.ExchangeSourceType);
            Assert.IsTrue(toVerify.Pair.BaseCurrency == pair.BaseCurrency);
            Assert.IsTrue(toVerify.Pair.CounterCurrency == pair.CounterCurrency);
            Assert.IsTrue(toVerify.TradeType == requestedOrderType);
        }
    }
}