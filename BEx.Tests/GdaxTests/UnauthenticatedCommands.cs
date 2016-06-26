using NUnit.Framework;

namespace BEx.Tests.GdaxTests
{
    [TestFixture]
    [Category("Gdax.UnauthenticatedCommands")]
    public class UnauthenticatedCommands : ExchangeVerificationBase
    {
        public UnauthenticatedCommands()
            : base(new Gdax())
        {
        }
        
        [Test]
        public void Gdax_GetOrderBook_BTCUSD_Success()
        {
            CommandVerification.RetrieveAndVerifyOrderBook(TestCandidate.DefaultPair);
        }

        [Test]
        public void Gdax_GetTick_BTCUSD_Success()
        {
            CommandVerification.RetrieveAndVerifyTick(TestCandidate.DefaultPair);
        }

        [Test]
        public void Gdax_GetTransactions_BTCUSD_Success()
        {
            CommandVerification.RetrieveAndVerifyTransactions(TestCandidate.DefaultPair);
        }
    }
}