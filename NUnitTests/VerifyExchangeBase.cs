using BEx;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NUnitTests
{
    public class VerifyExchangeBase
    {
        public Exchange toTest;

        private object orderLock;

        public static object testVelocityLock = new object();

        protected string APIKey
        {
            get;
            set;
        }

        protected string Secret
        {
            get;
            set;
        }

        protected string ClientID
        {
            get;
            set;
        }

        public VerifyExchangeBase(Type exchangeType)
        {
            orderLock = new object();

            GetAPIKeys(exchangeType);
        }

        private void GetAPIKeys(Type exchangeType)
        {
            XElement keyFile = XElement.Load(@"C:\_Work\BEx\TestingKeys.xml");

            XElement exchangeElement = null;

            switch (exchangeType.ToString())
            {
                case ("BEx.BitStamp"):
                    exchangeElement = keyFile.Element("BitStamp");
                    break;

                case ("BEx.Bitfinex"):
                    exchangeElement = keyFile.Element("BitFinex");
                    break;

                case ("BEx.BTCe"):
                    exchangeElement = keyFile.Element("BTCe");
                    break;
            }

            APIKey = exchangeElement.Element("Key").Value;
            Secret = exchangeElement.Element("Secret").Value;

            if (exchangeElement.Element("ClientID") != null)
                ClientID = exchangeElement.Element("ClientID").Value;
        }

        #region Verification Methods

        protected void VerifyAPIResult(APIResult toVerify)
        {
            Assert.IsNotNull(toVerify);

            Assert.IsTrue(toVerify.ExchangeTimeStamp > DateTime.MinValue);
            Assert.IsTrue(toVerify.ExchangeTimeStamp < DateTime.MaxValue);

            Assert.IsTrue(toVerify.LocalTimeStamp > DateTime.MinValue);
            Assert.IsTrue(toVerify.LocalTimeStamp < DateTime.MaxValue);
        }

        protected void VerifyTick()
        {
            foreach (KeyValuePair<Currency, HashSet<Currency>> pairSet in toTest.SupportedTradingPairs)
            {
                Currency baseCurrency = pairSet.Key;

                foreach (Currency counterCurrency in pairSet.Value)
                {
                    ThrottleTestVelocity();

                    Debug(string.Format("Verifying Tick for {0}/{1}", baseCurrency, counterCurrency));

                    Tick toVerify = toTest.GetTick(baseCurrency, counterCurrency);

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

        protected void VerifyOrderBook()
        {
            foreach (KeyValuePair<Currency, HashSet<Currency>> pairSet in toTest.SupportedTradingPairs)
            {
                Currency baseCurrency = pairSet.Key;

                foreach (Currency counterCurrency in pairSet.Value)
                {
                    ThrottleTestVelocity();

                    Debug(string.Format("Verifying OrderBook for {0}/{1}", baseCurrency, counterCurrency));

                    OrderBook toVerify = toTest.GetOrderBook(baseCurrency, counterCurrency);

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

        protected void VerifyTransactions()
        {
            foreach (KeyValuePair<Currency, HashSet<Currency>> pairSet in toTest.SupportedTradingPairs)
            {
                Currency baseCurrency = pairSet.Key;

                foreach (Currency counterCurrency in pairSet.Value)
                {
                    ThrottleTestVelocity();

                    Debug(string.Format("Verifying Transactions for {0}/{1}", baseCurrency, counterCurrency));

                    Transactions toVerify = toTest.GetTransactions(baseCurrency, counterCurrency);

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

        protected void VerifyAccountBalance()
        {
            ThrottleTestVelocity();

            AccountBalances toVerify = toTest.GetAccountBalance();

            VerifyAPIResult(toVerify);

            Assert.IsTrue(toVerify.Balances.Count > 0);

            foreach (Currency c in toTest.SupportedCurrencies)
            {
                Assert.IsTrue(toVerify.Balances.ContainsKey(c));
            }

        }

        protected void VerifyBuyOrder()
        {
            ThrottleTestVelocity();

            lock (orderLock)
            {
                Debug("Begin Buy Order Test");
                Order toVerify = toTest.CreateBuyOrder(1m, 5m);

                VerifyAPIResult(toVerify);

                Assert.IsTrue(toVerify.Type == OrderType.Buy);
                Assert.IsTrue(toVerify.Amount > 0.0m);
                Assert.IsTrue(toVerify.ID > 0);
                Assert.IsTrue(toVerify.Price > 0.0m);

                OpenOrders open = toTest.GetOpenOrders();

                Assert.IsTrue(open.Orders.Count > 0);

                Assert.IsNotNull(open.Orders.Find(x => x.ID == toVerify.ID));

                Debug(string.Format("Cancelling Buy Order {0}", toVerify.ID));
                bool cancelled = toTest.CancelOrder(toVerify.ID);

                Assert.IsTrue(cancelled);

                Debug(string.Format("Cancelled Sell Order {0}", toVerify.ID));

                open = toTest.GetOpenOrders();

                Assert.IsTrue(open.Orders.Count == 0);
            }
        }

        protected void VerifySellOrder()
        {
            ThrottleTestVelocity();

            lock (orderLock)
            {
                Debug("Begin Sell Order");
                Order toVerify = toTest.CreateSellOrder(0.02m, 10000m);

                VerifyAPIResult(toVerify);

                Assert.IsTrue(toVerify.Type == OrderType.Sell);
                Assert.IsTrue(toVerify.Amount > 0.0m);
                Assert.IsTrue(toVerify.ID > 0);
                Assert.IsTrue(toVerify.Price > 0.0m);

                OpenOrders open = toTest.GetOpenOrders();

                Assert.IsTrue(open.Orders.Count > 0);

                Assert.IsNotNull(open.Orders.Find(x => x.ID == toVerify.ID));

                Debug(string.Format("Cancelling Sell Order {0}", toVerify.ID));
                bool cancelled = toTest.CancelOrder(toVerify.ID);

                Assert.IsTrue(cancelled);

                Debug(string.Format("Cancelled Sell Order {0}", toVerify.ID));

                open = toTest.GetOpenOrders();

                Debug("Checking Open Orders");
                Assert.IsTrue(open.Orders.Count == 0);
            }
        }

        protected void VerifyOpenOrders()
        {
            OpenOrders toVerify = toTest.GetOpenOrders();

            VerifyAPIResult(toVerify);

            Assert.IsTrue(toVerify.Orders.Count > 0);

            foreach (Order ord in toVerify.Orders)
            {
                VerifyAPIResult(ord);
                Assert.IsTrue(ord.Amount > 0.0m);
                Assert.IsTrue(ord.ID > 0);
                Assert.IsTrue(ord.Price > 0.0m);
            }
        }

        protected void VerifyUserTransactions()
        {
            UserTransactions toVerify = toTest.GetUserTransactions();

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

        protected void VerifyDepositAddress()
        {
            DepositAddress address = toTest.GetDepositAddress();

            VerifyAPIResult(address);

            Assert.IsTrue(!string.IsNullOrEmpty(address.Address));
        }

        #endregion Verification Methods

        private void Debug(string message)
        {
            
            System.Diagnostics.Debug.WriteLine("{0}: {1}", this.toTest.GetType().ToString(), message);
        }

        /// <summary>
        /// Exchanges ban API access for those that make excessive requests,
        /// in order to avoid the banhammer let's slow down the pace of testing
        /// so that at most we make one request every 2 seconds.
        /// </summary>
        private void ThrottleTestVelocity()
        {
            lock (testVelocityLock)
            {
                new System.Threading.ManualResetEvent(false).WaitOne(2000);
            }
        }
    }
}