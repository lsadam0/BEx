// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BEx.UnitTests
{
    public class ExchangeExceptionVerification : ExchangeVerificationBase
    {
        public ExchangeExceptionVerification(Exchange testCandidate)
            : base()
        {
        }

        /*
        private Exchange GetInstance(string apiKey, string secret, string clientId)
        {
            if (base.testCandidateType == typeof(BEx.Bitfinex))
                return new Bitfinex(apiKey, secret);
            else if (base.testCandidateType == typeof(BEx.BitStamp))
                return new BitStamp(apiKey, secret, clientId);
            else
                return null;
        }

        public void MissingAPIKey_ExchangeAuthorizationException()
        {
            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    Exchange failure = GetInstance("", Secret, ClientId);
                });
        }

        public void IncorrectAPIKey_ExchangeAuthorizationException()
        {
            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    Exchange failure = GetInstance(ApiKey.Remove(ApiKey.Length - 1), Secret, ClientId);
                    failure.CreateSellOrder(9999, 9999);
                });
        }

        public void MissingSecretKey_ExchangeAuthorizationException()
        {
            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    Exchange failure = GetInstance(ApiKey, "", ClientId);
                });
        }

        public void IncorrectSecretKey_ExchangeAuthorizationException()
        {
            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    Exchange failure = GetInstance(ApiKey, Secret.Remove(Secret.Length - 1), ClientId);
                    failure.CreateSellOrder(9999, 9999);
                });
        }

        public void MissingClientID_ExchangeAuthorizationException()
        {
            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    Exchange failure = GetInstance(ApiKey, Secret, "");
                });
        }

        public void IncorrectClientID_ExchangeAuthorizationException()
        {
            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    Exchange failure = GetInstance(ApiKey, Secret, ClientId.Remove(this.ClientId.Length - 1));
                    failure.CreateSellOrder(9999, 9999);
                });
        }

        public void CreateSellOrder_InsufficientFundsException()
        {
            Assert.Throws<InsufficientFundsException>(
                delegate
                {
                    TestCandidate.CreateSellOrder(1200m, 99000.00m);
                });
        }

        public void CreateBuyOrder_InsufficientFundsException()
        {
            Assert.Throws<InsufficientFundsException>(
                delegate
                {
                    TestCandidate.CreateBuyOrder(1200m, 1.00m);
                });
        }*/
    }
}