// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace BEx.Tests.BitStampTests
{
    [TestFixture]
    [Category("BitStamp.AuthenticatedCommands")]
    public class BitStampAuthenticatedCommands : ExchangeVerificationBase
    {
        public BitStampAuthenticatedCommands() : base(ExchangeFactory.GetAuthenticatedExchange(ExchangeType.BitStamp))
        {
        }

        [TestFixtureSetUp]
        public void TestSetup()
        {
            Assert.IsInstanceOf<BitStamp>(TestCandidate);
        }

        [Test]
        public void BitStamp_GetAccountBalance_All_Success()
        {
            CommandVerification.VerifyAccountBalance();
        }

        [Test]
        public void BitStamp_GetDepositAddress_BTC_Success()
        {
            CommandVerification.VerifyDepositAddress(Currency.BTC);
        }

        [Test]
        public void BitStamp_GetOpenOrders_BTCUSD_Success()
        {
            CommandVerification.VerifyOpenOrders();
        }

        [Test]
        public void BitStamp_GetUserTransactions_BTCUSD_Success()
        {
            CommandVerification.VerifyUserTransactions(TestCandidate.DefaultPair);
        }

        [Test]
        public void Scratch()
        {
            var open = TestCandidate.GetOpenOrders();

            //  var sell = open.SellOrders.FirstOrDefault();

            var tick = TestCandidate.GetTick();
            //var confirm = base.TestCandidate.CancelOrder(sell.Value);

            {
            }
        }
    }
}