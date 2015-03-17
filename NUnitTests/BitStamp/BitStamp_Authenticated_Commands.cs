using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitStamp")]
    public class BitStamp_Authenticated_Commands : ExchangeVerificationBase
    {
        public BitStamp_Authenticated_Commands()
            : base(typeof(BEx.BitStamp))
        {
        }

        [Test]
        public void GetAccountBalance_All_Success()
        {
            CommandVerification.VerifyAccountBalance();
        }

        [Test]
        public void GetOpenOrders_BTCUSD_SUccess()
        {
            CommandVerification.VerifyOpenOrders();
        }

        [Test]
        public void GetUserTransactions_BTCUSD_Success()
        {
            CommandVerification.VerifyUserTransactions(testCandidate.DefaultPair);
        }

        [Test]
        public void GetDepositAddress_BTC_Success()
        {
            CommandVerification.VerifyDepositAddress(Currency.BTC);
        }
    }
}