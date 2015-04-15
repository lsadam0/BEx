// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using NUnit.Framework;
using BEx.Exceptions;
using BEx;

namespace BEx.UnitTests.BitStampTests
{
    [TestFixture]
    [Category("BitStamp.Exceptions")]
    public class BitStamp_Exceptions : ExchangeVerificationBase
    {
        BitStamp _testCandidate;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            _testCandidate = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.BitStamp) as BitStamp;
        }

        [Test]
        public void CreateBuyOrder_InsufficientFunds_LimitOrderRejectedException()
        {
            Assert.Throws<Exceptions.LimitOrderRejectedException>(() =>
                {
                    _testCandidate.CreateBuyLimitOrder(1000m, 1m);
                });
        }

        [Test]
        public void ExecuteAuthenticatedCommand_MissingAuthorization_ArgNull()
        {

            Assert.Throws<ArgumentNullException>(() =>
                {
                    BitStamp bits = new BitStamp("", "", "");
                    bits.GetAccountBalance();
                });

         
        }
     
        [Test]
        public void Constructor_IncorrectAuth_AuthException()
        {
            Assert.Throws<ExchangeAuthorizationException>(() =>
            {
                BitStamp bits = new BitStamp("somekey", "somesecret", "someclient");
                bits.GetAccountBalance();
            });

            Assert.Throws<ExchangeAuthorizationException>(() =>
            {
                AuthToken token = ExchangeFactory.GetBitstampAuthToken();
                BitStamp bits = new BitStamp(token.ApiKey, token.Secret, "someclientid");
                bits.GetAccountBalance();
            });

            Assert.Throws<ExchangeAuthorizationException>(() =>
            {
                AuthToken token = ExchangeFactory.GetBitstampAuthToken();
                BitStamp bits = new BitStamp(token.ApiKey, "somesecret", token.ClientId);
                bits.GetAccountBalance();
            });

            Assert.Throws<ExchangeAuthorizationException>(() =>
            {
                AuthToken token = ExchangeFactory.GetBitstampAuthToken();
                BitStamp bits = new BitStamp("somekey", token.Secret, token.ClientId);
                bits.GetAccountBalance();
            });
        }

        [Test]
        public void Constructor_AuthNotSet_AuthException()
        {
            Assert.Throws<ExchangeAuthorizationException>(() =>
            {
                BitStamp bits = new BitStamp();
                bits.GetAccountBalance();
            });
        }       

        /*
         * 
         * 
        public BitStamp_Exceptions()
            : base(typeof(BEx.BitStamp))
        {
        }

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
        public void Constructor_MissingClientID_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.MissingClientID_ExchangeAuthorizationException();
        }

        [Test]
        public void AuthenticatedCommand_IncorrectClientID_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.IncorrectClientID_ExchangeAuthorizationException();
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
        }*/
    }
}