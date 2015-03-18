using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitStamp.UnauthenticatedCommands")]
    public class BitStampUnauthenticatedCommands : ExchangeVerificationBase
    {
        public BitStampUnauthenticatedCommands()
            : base(typeof(BEx.BitStamp))
        {
        }

        [Test]
        public void GetTick_BTCUSD_Success()
        {
            CommandVerification.VerifyTick(testCandidate.DefaultPair);
        }

        [Test]
        public void GetOrderBook_BTCUSD_Success()
        {
            CommandVerification.VerifyOrderBook(testCandidate.DefaultPair);
        }

        [Test]
        public void GetTransactions_BTCUSD_Success()
        {
            CommandVerification.VerifyTransactions(testCandidate.DefaultPair);
        }
    }
}