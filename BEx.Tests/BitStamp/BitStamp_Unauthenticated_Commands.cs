// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace BEx.UnitTests.BitStampTests
{
    [TestFixture]
    [Category("BitStamp.UnauthenticatedCommands")]
    public class BitStampUnauthenticatedCommands
    {
        private ExchangeCommandVerification _commandVerification;
        private BitStamp _testCandidate;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            _testCandidate = ExchangeFactory.GetUnauthenticatedExchange(ExchangeType.BitStamp) as BitStamp;
            Assert.IsInstanceOf<BitStamp>(_testCandidate);
            _commandVerification = new ExchangeCommandVerification(_testCandidate);
        }

        [Test]
        public void BitStamp_GetOrderBook_BTCUSD_Success()
        {
            _commandVerification.VerifyOrderBook(_testCandidate.DefaultPair);
        }

        [Test]
        public void BitStamp_GetTick_BTCUSD_Success()
        {
            _commandVerification.VerifyTick(_testCandidate.DefaultPair);
        }

        [Test]
        public void BitStamp_GetTransactions_BTCUSD_Success()
        {
            _commandVerification.VerifyTransactions(_testCandidate.DefaultPair);
        }
    }
}