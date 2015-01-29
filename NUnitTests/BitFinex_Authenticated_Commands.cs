using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitFinex.Authenticated")]
    public class BitFinex_Authenticated_Commands : VerifyExchangeBase
    {
        public BitFinex_Authenticated_Commands()
            : base(typeof(BEx.Bitfinex))
        {
            toTest = new Bitfinex(base.APIKey, base.Secret);
        }

        #region Account Balance Tests

        [Test]
        public void BitFinex_GetAccountBalance()
        {
            VerifyAccountBalance();
        }

        #endregion Account Balance Tests

        #region Deposit Address

        [Test]
        public void BitFinex_GetDepositAddress()
        {
            VerifyDepositAddress();
        }



        #endregion Deposit Address

        [Test]
        public void BitFinex_GetUserTransactions()
        {

            VerifyUserTransactions();
        }

        #region Orders

        [Test]
        public void BitFinex_CreateSellOrder()
        {
            Order o = toTest.CreateSellOrder(Currency.BTC, Currency.USD, 0.02m, 10000.00m);

            VerifySellOrder();

            OpenOrders open = toTest.GetOpenOrders();

            Assert.IsTrue(open.Orders.Count > 0);

            Assert.IsNotNull(open.Orders.Find(x => x.ID == o.ID));

            bool cancelled = toTest.CancelOrder(o.ID);

            Assert.IsTrue(cancelled);

            open = toTest.GetOpenOrders();

            Assert.IsTrue(open.Orders.Count == 0);


        }

        [Test]
        public void BitFinex_CreateBuyOrder()
        {
            Order o = toTest.CreateBuyOrder(Currency.BTC, Currency.USD, 0.01m, 456.00m);

            VerifyBuyOrder();
        }

        [Test]
        public void BitFinex_GetOpenOrders()
        {
            
            VerifyOpenOrders();
        }

        #endregion Orders
    }
}