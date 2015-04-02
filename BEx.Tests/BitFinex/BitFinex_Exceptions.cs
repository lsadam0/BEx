// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using NUnit.Framework;
using BEx;

namespace BEx.UnitTests.BitfinexTests
{
    [TestFixture]
    [Category("Bitfinex.Exceptions")]
    public class Bitfinex_Exceptions : ExchangeVerificationBase
    {
        private Bitfinex _exchange;


        [TestFixtureSetUp]
        public void Setup()
        {
            _exchange = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.Bitfinex) as Bitfinex;
        }

        [Test]
        public void CreateBuyOrder_InsufficientFunds_LimitOrderRejectedException()
        {
            Assert.Throws<Exceptions.LimitOrderRejectedException>(() =>
                {
                    Order res = _exchange.CreateBuyOrder(1999, 1);
                    { }
                });
        }
        /*
        [Test]
        public void Constructor_MissingAPIKey_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.MissingAPIKey_ExchangeAuthorizationException();
        }

        [Test]
        public void AuthenticatedCommand_IncorrectAPIKey_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.IncorrectAPIKey_ExchangeAuthorizationException();
        }

        [Test]
        public void Constructor_MissingSecretKey_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.MissingSecretKey_ExchangeAuthorizationException();
        }

        [Test]
        public void AuthenticatedCommand_IncorrectSecretKey_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.IncorrectSecretKey_ExchangeAuthorizationException();
        }

        [Test]
        public void CreateSellOrder_InsufficientFunds_InsufficientFundsException()
        {
            base.ExceptionVerification.CreateSellOrder_InsufficientFundsException();
        }

        [Test]
        public void CreateBuyOrder_InsufficientFunds_InsufficientFundsException()
        {
            base.ExceptionVerification.CreateBuyOrder_InsufficientFundsException();
        }
         */
    }
}