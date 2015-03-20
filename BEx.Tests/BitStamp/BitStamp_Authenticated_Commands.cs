// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BEx;
using NUnit.Framework;

namespace BEx.UnitTests
{
    [TestFixture]
    [Category("BitStamp.AuthenticatedCommands")]
    public class BitStamp_Authenticated_Commands : ExchangeVerificationBase
    {
        private ExchangeCommandVerification commandVerification;
        public BitStamp_Authenticated_Commands()
            : base()
        {
            testCandidate = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.BitStamp) as Exchange;
            commandVerification = new ExchangeCommandVerification(testCandidate);
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
            commandVerification.VerifyUserTransactions(testCandidate.DefaultPair);
        }

        [Test]
        public void BitStamp_GetDepositAddress_BTC_Success()
        {
            commandVerification.VerifyDepositAddress(Currency.BTC);
        }
    }
}