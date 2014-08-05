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
    public struct TestStruct
    {
        public Currency baseC;
        public Currency? counterC;

        public TestStruct(Currency aa)
        {
            baseC = Currency.BTC;
            counterC = Currency.BTN;
        }

    }

    [TestFixture]
    [Category("BitStamp")]
    public class BitStamp_Commands : VerifyExchangeBase
    {
        public BitStamp_Commands()
            : base(typeof(BEx.BitStamp))
        {
            toTest = new BitStamp();

            toTest.APIKey = base.APIKey;
            toTest.SecretKey = base.Secret;
            toTest.ClientID = base.ClientID;
        }

        [Test]
        public void BitStamp_GetTick_BTCUSD()
        {
            Tick t = toTest.GetTick();

            VerifyTick(t, Currency.BTC, Currency.USD);
        }


        [Test]
        public void BitStamp_GetOrderBook_BTCUSD()
        {

            OrderBook o = toTest.GetOrderBook();

            VerifyOrderBook(o);
        }

        [Test]
        public void BitStamp_GetTransactions_BTCUSD()
        {

            Transactions trans = toTest.GetTransactions();

            Assert.IsNotNull(trans);
        }


        [Test]
        public void BitStamp_GetAccountBalance()
        {
            AccountBalance res = toTest.GetAccountBalance();

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
            object res = toTest.CreateSellOrder(0.01m, 1200.00m);

            VerifySellOrder(res);
        }

        [Test]
        public void BitStamp_GetOpenOrders()
        {
            object res = toTest.GetOpenOrders();

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
            string address = toTest.GetDepositAddress();

            Assert.IsTrue(!String.IsNullOrEmpty(address));

        }

        

        [Test]
        public void StructTest()
        {
            TestStruct A = new TestStruct();
            TestStruct B = A;

            A.counterC = null;
            A.baseC = Currency.BOB;

            { }



        }
    }
}
