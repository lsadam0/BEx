using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitFinex.Unauthenticated")]
    public class BitFinex_Unauthenticated_Commands : VerifyExchangeBase
    {
        public BitFinex_Unauthenticated_Commands()
            : base(typeof(BEx.Bitfinex))
        {
            toTest = new Bitfinex(base.APIKey, base.Secret);
        }

        [Test]
        public void BitFinex_GetTick()
        {
            VerifyTick();
        }

        #region OrderBook Tests

        [Test]
        public void BitFinex_GetOrderBook()
        {
            VerifyOrderBook();
        }

        #endregion OrderBook Tests

        #region Transaction Tests

        [Test]
        public void BitFinex_GetTransactions()
        {
            VerifyTransactions();
        }

        #endregion Transaction Tests
    }
}