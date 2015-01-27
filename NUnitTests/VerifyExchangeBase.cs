using BEx;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace NUnitTests
{
    public class VerifyExchangeBase
    {
        public Exchange toTest;

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

        public static object testVelocityLock = new object();

        /// <summary>
        /// Exchanges ban API access for those that make excessive requests,
        /// in order to avoid the banhammer let's slow down the pace of testing
        /// so that at most we make one request every 2 seconds.
        /// </summary>
        protected void ThrottleTestVelocity()
        {
            lock (testVelocityLock)
            {
                new System.Threading.ManualResetEvent(false).WaitOne(3000);
            }
        }

        protected void VerifyTick(Tick toVerify, Currency baseC, Currency counterC)
        {
            ThrottleTestVelocity();

            Assert.IsNotNull(toVerify);

            Assert.IsTrue(toVerify.BaseCurrency == baseC);
            Assert.IsTrue(toVerify.CounterCurrency == counterC);
            Assert.IsTrue(toVerify.Ask > 0.0m);
            Assert.IsTrue(toVerify.Bid > 0.0m);
            Assert.IsTrue(toVerify.Last > 0.0m);
            Assert.IsTrue(toVerify.High > 0.0m);
            Assert.IsTrue(toVerify.Low > 0.0m);
            Assert.IsTrue(toVerify.Volume > 0.0m);
        }

        protected void VerifyOrderBook(OrderBook toVerify)
        {
            ThrottleTestVelocity();

            Assert.IsNotNull(toVerify);

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
        }

        protected void VerifyTransactions(Transactions toVerify)
        {
            ThrottleTestVelocity();

            Assert.IsNotNull(toVerify);


            Assert.IsTrue(toVerify.TransactionsCollection.Count > 0);

            foreach (Transaction t in toVerify.TransactionsCollection)
            {
                Assert.IsTrue(t.Amount > 0.0m);
                Assert.IsTrue(t.Price > 0.0m);
                Assert.IsTrue(t.TransactionID > 0);
            }
        }

        protected void VerifyAccountBalance(AccountBalances toVerify)
        {
            ThrottleTestVelocity();

            Assert.IsNotNull(toVerify);

            Assert.IsTrue(toVerify.Balances != null);
            Assert.IsTrue(toVerify.Balances.Count > 0);

            //foreach (AccountBalance balance in toVerify.Balances)
            //{
            //  Assert.IsTrue(balance.Timestamp < DateTime.Now);
            //}
        }

        protected void VerifyBuyOrder(Order toVerify)
        {
            ThrottleTestVelocity();

            Assert.IsTrue(toVerify.Type == OrderType.Buy);
            Assert.IsTrue(toVerify.Amount > 0.0m);
            Assert.IsTrue(toVerify.ID > 0);
            Assert.IsTrue(toVerify.Price > 0.0m);

            Assert.IsNotNull(toVerify);
        }

        protected void VerifySellOrder(Order toVerify)
        {
            ThrottleTestVelocity();

            Assert.IsTrue(toVerify.Type == OrderType.Sell);
            Assert.IsTrue(toVerify.Amount > 0.0m);
            Assert.IsTrue(toVerify.ID > 0);
            Assert.IsTrue(toVerify.Price > 0.0m);

            Assert.IsNotNull(toVerify);
        }

        protected void VerifyOpenOrders(OpenOrders toVerify)
        {
            Assert.IsNotNull(toVerify);

            Assert.IsTrue(toVerify.Orders.Count > 0);

            foreach (Order ord in toVerify.Orders)
            {
                Assert.IsTrue(ord.Amount > 0.0m);
                Assert.IsTrue(ord.ID > 0);
                Assert.IsTrue(ord.Price > 0.0m);
            }
        }

        protected void VerifyUserTransactions(UserTransactions toVerify)
        {
            Assert.IsNotNull(toVerify);
            Assert.IsNotNull(toVerify.UserTrans);
            Assert.IsTrue(toVerify.UserTrans.Count > 0);

            foreach (UserTransaction transaction in toVerify.UserTrans)
            {
                // Assert.IsTrue(transaction.BaseCurrencyAmount > 0.0m);
                // Assert.IsTrue(transaction.CounterCurrencyAmount > 0.0m);
                // Assert.IsTrue(transaction.ExchangeRate > 0.0m);
                Assert.IsTrue(transaction.ID > 0);
                Assert.IsTrue(transaction.OrderID != null);
                Assert.IsTrue(transaction.OrderID > 0);
            }
            Assert.IsNotNull(toVerify);
        }

        protected void VerifyDepositAddress(DepositAddress address)
        {
            Assert.IsNotNull(address);

            Assert.IsTrue(!string.IsNullOrEmpty(address.Address));
        }
    }
}