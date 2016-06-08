﻿// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;

namespace BEx.Tests.GdaxTests
{
    [TestFixture]
    [Category("Gdax.AuthenticatedCommands")]
    public class GdaxAuthenticatedCommands : ExchangeVerificationBase
    {
        public GdaxAuthenticatedCommands() : base(ExchangeFactory.GetAuthenticatedExchange(ExchangeType.Gdax))
        {
        }

        [TestFixtureSetUp]
        public void TestSetup()
        {
            Assert.IsInstanceOf<Gdax>(TestCandidate);
        }

        [Test]
        public void Gdax_GetAccountBalance_All_Success()
        {
            CommandVerification.VerifyAccountBalance();
        }

        [Test]
        public void Gdax_GetDepositAddress_BTC_Success()
        {
            CommandVerification.VerifyDepositAddress(Currency.BTC);
        }

        [Test]
        public void Gdax_GetOpenOrders_BTCUSD_Success()
        {
            CommandVerification.VerifyOpenOrders();
        }

        [Test]
        public void Gdax_GetUserTransactions_BTCUSD_Success()
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