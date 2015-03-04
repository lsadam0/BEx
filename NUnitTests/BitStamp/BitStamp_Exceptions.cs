using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitStamp.Exceptions")]
    public class BitStamp_Exceptions : ExchangeVerificationBase
    {
        public BitStamp_Exceptions()
            : base(typeof(BEx.BitStamp))
        {
        }

        [Test]
        public void BitStamp_MissingAPIKey_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.MissingAPIKey_ExchangeAuthorizationException();
        }

        [Test]
        public void BitStamp_IncorrectAPIKey_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.IncorrectAPIKey_ExchangeAuthorizationException();
        }

        [Test]
        public void BitStamp_MissingSecretKey_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.MissingSecretKey_ExchangeAuthorizationException();
        }

        [Test]
        public void BitStamp_IncorrectSecretKey_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.IncorrectSecretKey_ExchangeAuthorizationException();
        }

        [Test]
        public void BitStamp_MissingClientID_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.MissingClientID_ExchangeAuthorizationException();
        }

        [Test]
        public void BitStamp_IncorrectClientID_ExchangeAuthorizationException()
        {
            base.ExceptionVerification.IncorrectClientID_ExchangeAuthorizationException();
        }

        [Test]
        public void BitStamp_CreateSellOrder_InsufficientFundsException()
        {
            base.ExceptionVerification.CreateSellOrder_InsufficientFundsException();
        }

        [Test]
        public void BitStamp_CreateBuyOrder_InsufficientFundsException()
        {
            base.ExceptionVerification.CreateBuyOrder_InsufficientFundsException();
        }
    }
}