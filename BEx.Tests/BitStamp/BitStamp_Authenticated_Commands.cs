// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace BEx.UnitTests.BitStampTests
{
    [TestFixture]
    [Category("BitStamp.AuthenticatedCommands")]
    public class BitStamp_Authenticated_Commands
    {
        private ExchangeCommandVerification _commandVerification;
        private BitStamp _testCandidate;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            _testCandidate = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.BitStamp) as BitStamp;
            Assert.IsInstanceOf<BitStamp>(_testCandidate);
            _commandVerification = new ExchangeCommandVerification(_testCandidate);
        }

        [Test]
        public void BitStamp_GetAccountBalance_All_Success()
        {
            _commandVerification.VerifyAccountBalance();
        }

        [Test]
        public void BitStamp_GetDepositAddress_BTC_Success()
        {
            _commandVerification.VerifyDepositAddress(Currency.BTC);
        }

        [Test]
        public void BitStamp_GetOpenOrders_BTCUSD_Success()
        {
            _commandVerification.VerifyOpenOrders();
        }

        [Test]
        public void BitStamp_GetUserTransactions_BTCUSD_Success()
        {
            _commandVerification.VerifyUserTransactions(_testCandidate.DefaultPair);
        }
    }
}