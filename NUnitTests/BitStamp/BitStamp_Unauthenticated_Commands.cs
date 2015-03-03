using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitStamp.Unauthenticated")]
    public class BitStamp_Unauthenticated_Commands : ExchangeVerificationBase
    {
        public BitStamp_Unauthenticated_Commands()
            : base(typeof(BEx.BitStamp))
        {
        }

        [Test]
        public void BitStamp_GetTick()
        {
            CommandVerification.VerifyTick();
        }

        [Test]
        public void BitStamp_GetOrderBook()
        {
            CommandVerification.VerifyOrderBook();
        }

        [Test]
        public void BitStamp_GetTransactions()
        {
            CommandVerification.VerifyTransactions();
        }
    }
}