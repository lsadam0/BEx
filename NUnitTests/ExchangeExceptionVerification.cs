using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using BEx;

namespace NUnitTests
{

    public class ExchangeExceptionVerification : ExchangeVerificationBase
    {
        public ExchangeExceptionVerification(Exchange testCandidate)
            : base(testCandidate)
        {


        }

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
                    Exchange failure = GetInstance("", Secret, ClientID);
                });

        }

        public void IncorrectAPIKey_ExchangeAuthorizationException()
        {

            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    Exchange failure = GetInstance(APIKey.Remove(APIKey.Length - 1), Secret, ClientID);
                    failure.CreateSellOrder(9999, 9999);

                });

        }

        public void MissingSecretKey_ExchangeAuthorizationException()
        {
            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    Exchange failure = GetInstance(APIKey, "", ClientID);
                });

        }

        public void IncorrectSecretKey_ExchangeAuthorizationException()
        {

            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    Exchange failure = GetInstance(APIKey, Secret.Remove(Secret.Length - 1), ClientID);
                    failure.CreateSellOrder(9999, 9999);
                });

        }

        public void MissingClientID_ExchangeAuthorizationException()
        {

            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    Exchange failure = GetInstance(APIKey, Secret, "");
                });

        }

        public void IncorrectClientID_ExchangeAuthorizationException()
        {


            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    Exchange failure = GetInstance(APIKey, Secret, ClientID.Remove(this.ClientID.Length - 1));
                    failure.CreateSellOrder(9999, 9999);
                });

        }

        public void CreateSellOrder_InsufficientFundsException()
        {

            Assert.Throws<InsufficientFundsException>(
                delegate
                {
                    testCandidate.CreateSellOrder(1200m, 99000.00m);
                });

        }

        public void CreateBuyOrder_InsufficientFundsException()
        {


            Assert.Throws<InsufficientFundsException>(
                delegate
                {
                    testCandidate.CreateBuyOrder(1200m, 1.00m);
                });

        }
    }
}
