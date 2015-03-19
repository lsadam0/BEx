using NUnit.Framework;

namespace BEx.UnitTests
{
    [TestFixture]
    [Category("Bitfinex.Exceptions")]
    public class Bitfinex_Exceptions : ExchangeVerificationBase
    {
        public Bitfinex_Exceptions()
            : base(typeof(BEx.Bitfinex))
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
        public void CreateSellOrder_InsufficientFunds_InsufficientFundsException()
        {
            base.ExceptionVerification.CreateSellOrder_InsufficientFundsException();
        }

        [Test]
        public void CreateBuyOrder_InsufficientFunds_InsufficientFundsException()
        {
            base.ExceptionVerification.CreateBuyOrder_InsufficientFundsException();
        }
    }
}