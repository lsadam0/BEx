// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace BEx.UnitTests.BitStampTests
{
    [TestFixture]
    [Category("BitStamp.UnauthenticatedCommands")]
    public class BitStampUnauthenticatedCommands
    {
        private ExchangeCommandVerification commandVerification;
        private BitStamp TestCandidate;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            TestCandidate = ExchangeFactory.GetUnauthenticatedExchange(ExchangeType.BitStamp) as BitStamp;
            Assert.IsInstanceOf<BitStamp>(TestCandidate);
            commandVerification = new ExchangeCommandVerification(TestCandidate);
        }

        [Test]
        public void BitStamp_GetOrderBook_BTCUSD_Success()
        {
            commandVerification.VerifyOrderBook(TestCandidate.DefaultPair);
        }

        [Test]
        public void BitStamp_GetTick_BTCUSD_Success()
        {
            commandVerification.VerifyTick(TestCandidate.DefaultPair);
        }

        [Test]
        public void BitStamp_GetTransactions_BTCUSD_Success()
        {
            commandVerification.VerifyTransactions(TestCandidate.DefaultPair);
        }
    }
}