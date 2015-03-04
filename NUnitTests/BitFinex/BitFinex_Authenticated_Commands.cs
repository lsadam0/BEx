using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitFinex.Authenticated")]
    public class BitFinex_Authenticated_Commands : ExchangeVerificationBase
    {
        public BitFinex_Authenticated_Commands()
            : base(typeof(BEx.Bitfinex))
        {
           
        }

        #region Account Balance Tests

        [Test]
        public void BitFinex_GetAccountBalance()
        {
            CommandVerification.VerifyAccountBalance();

        }

        #endregion Account Balance Tests

        #region Deposit Address

        [Test]
        public void BitFinex_GetBTCDepositAddress()
        {
            CommandVerification.VerifyDepositAddress(Currency.BTC);
        }

        [Test]
        public void BitFinex_GetLTCDepositAddress()
        {
            CommandVerification.VerifyDepositAddress(Currency.LTC);
        }

        [Test]
        public void BitFinex_GetDRKDepositAddress()
        {
            CommandVerification.VerifyDepositAddress(Currency.DRK);
        }
        #endregion Deposit Address

        [Test]
        public void BitFinex_GetUserTransactions()
        {
            CommandVerification.VerifyUserTransactions();
        }

        #region Orders
        /*
        [Test]
        public void BitFinex_CreateSellOrder()
        {
            CommandVerification.VerifySellOrder();
        }
        */
        /*
        [Test]
        public void BitFinex_CreateBuyOrder()
        {
            Order o = testCandidate.CreateBuyOrder(Currency.BTC, Currency.USD, 0.01m, 456.00m);

            CommandVerification.VerifyBuyOrder();
        }
        */
        [Test]
        public void BitFinex_GetOpenOrders()
        {
            CommandVerification.VerifyOpenOrders();
        }

        #endregion Orders
    }
}