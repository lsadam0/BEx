// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace BEx.UnitTests
{
    [TestFixture]
    [Category("BitStamp.UnauthenticatedCommands")]
    public class BitStampUnauthenticatedCommands : ExchangeVerificationBase
    {
        private ExchangeCommandVerification commandVerification;
        public BitStampUnauthenticatedCommands()
            : base()
        {
            testCandidate = ExchangeFactory.GetUnauthenticatedExchange(ExchangeType.BitStamp) as Exchange;
            commandVerification = new ExchangeCommandVerification(testCandidate);
        }

        [Test]
        public void BitStamp_GetTick_BTCUSD_Success()
        {
            
            commandVerification.VerifyTick(testCandidate.DefaultPair);
        }

        [Test]
        public void BitStamp_GetOrderBook_BTCUSD_Success()
        {
            commandVerification.VerifyOrderBook(testCandidate.DefaultPair);
        }

        [Test]
        public void BitStamp_GetTransactions_BTCUSD_Success()
        {
            commandVerification.VerifyTransactions(testCandidate.DefaultPair);
        }
    }
}