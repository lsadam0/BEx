﻿// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace BEx.Tests.BitStampTests
{
    [TestFixture]
    [Category("BitStamp.UnauthenticatedCommands")]
    public class BitStampUnauthenticatedCommands : ExchangeVerificationBase
    {
        public BitStampUnauthenticatedCommands()
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
            CommandVerification.VerifyOrderBook(TestCandidate.DefaultPair);
        }

        [Test]
        public void BitStamp_GetTick_BTCUSD_Success()
        {
            CommandVerification.VerifyTick(TestCandidate.DefaultPair);
        }

        [Test]
        public void BitStamp_GetDayRange_BTCUSD_Success()
        {
            CommandVerification.VerifyDayRange(TestCandidate.DefaultPair);
        }

        [Test]
        public void BitStamp_GetTransactions_BTCUSD_Success()
        {
            CommandVerification.VerifyTransactions(TestCandidate.DefaultPair);
        }
    }
}