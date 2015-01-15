using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using NUnit.Framework;

using BEx;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitStamp.Authenticated")]
    public class BitStamp_Authenticated_Commands : VerifyExchangeBase
    {

        public BitStamp_Authenticated_Commands() : base(typeof(BEx.BitStamp))
        {
            toTest = new BitStamp(base.APIKey, base.Secret, base.ClientID);

        }

        [Test]
        public void BitStamp_GetAccountBalance()
        {
            AccountBalances res = toTest.GetAccountBalance();

            VerifyAccountBalance(res);

        }

        [Test]
        public void BitStamp_CreateBuyOrder()
        {
            object res = toTest.CreateBuyOrder(100.0m, 100.99m);

            VerifyBuyOrder(res);
        }

        [Test]
        public void BitStamp_CreateSellOrder()
        {
            Order res = toTest.CreateSellOrder(1000m, 1200.00m);

            VerifySellOrder(res);
        }

        [Test]
        public void BitStamp_GetOpenOrders()
        {
            OpenOrders res = toTest.GetOpenOrders();

            VerifyOpenOrders(res);
        }

        [Test]
        public void BitStamp_GetUserTransactions()
        {
            UserTransactions res = toTest.GetUserTransactions();

            VerifyUserTransactions(res);
        }

        [Test]
        public void BitStamp_CancelOrder()
        {
            OpenOrders orders = toTest.GetOpenOrders();

            Order toCancel = orders.Orders[0];

            Assert.IsTrue(toTest.CancelOrder(toCancel));

            { }
        }

        [Test]
        public void BitStamp_GetDepositAddress()
        {
            DepositAddress address = toTest.GetDepositAddress();

            //Assert.IsTrue(!String.IsNullOrEmpty(address));

        }

        [Test]
        public void BitStamp_GetPendingDeposits()
        {
            PendingDeposits d = toTest.GetPendingDeposits();

            Assert.IsNotNull(d);
        }

        [Test]
        public void BitStamp_GetPendingWithdrawals()
        {
            PendingWithdrawals w = toTest.GetPendingWithdrawals();

            Assert.IsNotNull(w);
        }
    }
}
