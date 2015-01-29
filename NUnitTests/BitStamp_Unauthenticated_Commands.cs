using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitStamp.Unauthenticated")]
    public class BitStamp_Unauthenticated_Commands : VerifyExchangeBase
    {
        public BitStamp_Unauthenticated_Commands()
            : base(typeof(BEx.BitStamp))
        {
            toTest = new BitStamp(base.APIKey, base.Secret, base.ClientID);
        }

        [Test]
        public void BitStamp_GetTick()
        {

            VerifyTick();
        }

        [Test]
        public void BitStamp_GetOrderBook()
        {

            VerifyOrderBook();
        }

        [Test]
        public void BitStamp_GetTransactions()
        {
            VerifyTransactions();
        }
    }
}