using BEx;
using NUnit.Framework;
using System;
using System.Threading;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitStamp.OrderProcess")]
    public class BitStamp_OrderProcess : VerifyExchangeBase
    {
        private Semaphore buyOrderBlock = new Semaphore(1, 1);
        private Semaphore sellOrderBlock = new Semaphore(1, 1);

        public BitStamp_OrderProcess()
            : base(typeof(BEx.BitStamp))
        {
            toTest = new BitStamp(base.APIKey, base.Secret, base.ClientID);

            DetermineTestOrder();
        }

        private void DetermineTestOrder()
        {
            OpenOrders ordersToCancel = toTest.GetOpenOrders();

            foreach (Order toCancel in ordersToCancel.Orders)
            {
                Assert.IsTrue(toTest.CancelOrder(toCancel));
            }

            AccountBalances current = toTest.GetAccountBalance();

            Decimal btcBalance = 0.0m;
            Decimal usdBalance = 0.0m;

            //   sellOrderBlock.Release(1);
        }

        [Test]
        public void BitStamp_CreateBuyOrder()
        {
            //    buyOrderBlock.WaitOne(30000);

            { }
        }

        [Test]
        public void BitStamp_CreateSellOrder()
        {
            //      sellOrderBlock.WaitOne(30000);

            { }
        }
    }
}