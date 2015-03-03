using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitFinex.Unauthenticated")]
    public class BitFinex_Unauthenticated_Commands : ExchangeVerificationBase
    {
        public BitFinex_Unauthenticated_Commands()
            : base(typeof(BEx.Bitfinex))
        {
        }

        [Test]
        public void BitFinex_GetTick()
        {
            CommandVerification.VerifyTick();
        }

        #region OrderBook Tests

        [Test]
        public void BitFinex_GetOrderBook()
        {
            CommandVerification.VerifyOrderBook();
        }

        #endregion OrderBook Tests

        #region Transaction Tests

        [Test]
        public void BitFinex_GetTransactions()
        {
            CommandVerification.VerifyTransactions();
        }

        #endregion Transaction Tests
    }
}