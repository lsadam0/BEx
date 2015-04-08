// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx;
using NUnit.Framework;

namespace BEx.UnitTests.BitStampTests
{
    [TestFixture]
    [Category("BitStamp.AuthenticatedCommands")]
    public class BitStamp_Authenticated_Commands
    {
        private ExchangeCommandVerification commandVerification;
        private BitStamp TestCandidate;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            TestCandidate = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.BitStamp) as BitStamp;
            Assert.IsInstanceOf<BitStamp>(TestCandidate);
            commandVerification = new ExchangeCommandVerification(TestCandidate);

        }

        [Test]
        public void BitStamp_GetAccountBalance_All_Success()
        {
            commandVerification.VerifyAccountBalance();
        }

        [Test]
        public void BitStamp_GetOpenOrders_BTCUSD_Success()
        {
            commandVerification.VerifyOpenOrders();
        }

        [Test]
        public void BitStamp_GetUserTransactions_BTCUSD_Success()
        {
            commandVerification.VerifyUserTransactions(TestCandidate.DefaultPair);
        }

        [Test]
        public void BitStamp_GetDepositAddress_BTC_Success()
        {
            commandVerification.VerifyDepositAddress(Currency.BTC);
        }
    }
}