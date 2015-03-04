using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitStamp.Authenticated")]
    public class BitStamp_Authenticated_Commands : ExchangeVerificationBase
    {
        public BitStamp_Authenticated_Commands()
            : base(typeof(BEx.BitStamp))
        {
        }

        [Test]
        public void BitStamp_GetAccountBalance()
        {
            CommandVerification.VerifyAccountBalance();
        }

        /*

        [Test]
        public void BitStamp_CreateBuyOrder()
        {
            CommandVerification.VerifyBuyOrder();
        }

        [Test]
        public void BitStamp_CreateSellOrder()
        {
            CommandVerification.VerifySellOrder();
        }
        */

        [Test]
        public void BitStamp_GetOpenOrders()
        {
            CommandVerification.VerifyOpenOrders();
        }

        [Test]
        public void BitStamp_GetUserTransactions()
        {
            CommandVerification.VerifyUserTransactions();
        }

        [Test]
        public void BitStamp_GetBTCDepositAddress()
        {
            CommandVerification.VerifyDepositAddress(Currency.BTC);
        }
    }
}