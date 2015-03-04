using NUnit.Framework;

namespace NUnitTests
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
        public void BitFinex_MissingAPIKey_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.MissingAPIKey_ExchangeAuthorizationException();
        }

        [Test]
        public void BitFinex_IncorrectAPIKey_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.IncorrectAPIKey_ExchangeAuthorizationException();
        }

        [Test]
        public void BitFinex_MissingSecretKey_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.MissingSecretKey_ExchangeAuthorizationException();
        }

        [Test]
        public void BitFinex_IncorrectSecretKey_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.IncorrectSecretKey_ExchangeAuthorizationException();
        }

        [Test]
        public void BitFinex_CreateSellOrder_InsufficientFundsException()
        {
            base.ExceptionVerification.CreateSellOrder_InsufficientFundsException();
        }

        [Test]
        public void BitFinex_CreateBuyOrder_InsufficientFundsException()
        {
            base.ExceptionVerification.CreateBuyOrder_InsufficientFundsException();
        }
    }
}