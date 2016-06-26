// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace BEx.Tests.BitStampTests
{
    [TestFixture]
    [Category("BitStamp.UnauthenticatedCommands")]
    public class UnauthenticatedCommands : ExchangeVerificationBase
    {
        public UnauthenticatedCommands()
            : base(ExchangeFactory.GetUnauthenticatedExchange(ExchangeType.BitStamp))
        {
        }

        [TestFixtureSetUp]
        public void TestSetup()
        {
            Assert.IsInstanceOf<BitStamp>(TestCandidate);
        }
        

        [Test]
        public void BitStamp_GetOrderBook_BTCUSD_Success()
        {
            CommandVerification.RetrieveAndVerifyOrderBook(TestCandidate.DefaultPair);
        }

        [Test]
        public void BitStamp_GetTick_BTCUSD_Success()
        {
            CommandVerification.RetrieveAndVerifyTick(TestCandidate.DefaultPair);
        }

        [Test]
        public void BitStamp_GetTransactions_BTCUSD_Success()
        {
            CommandVerification.RetrieveAndVerifyTransactions(TestCandidate.DefaultPair);
        }
    }
}