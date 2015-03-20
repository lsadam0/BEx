using BEx;
using System;
using System.Xml.Linq;

namespace BEx.UnitTests
{
    public class ExchangeVerificationBase
    {
        protected Exchange testCandidate
        {
            get;
            set;
        }

        protected Type testCandidateType
        {
            get;
            set;
        }

        protected string ApiKey
        {
            get;
            set;
        }

        protected string Secret
        {
            get;
            set;
        }

        protected string ClientId
        {
            get;
            set;
        }

        private ExchangeCommandVerification commandVerification = null;

        public ExchangeCommandVerification CommandVerification
        {
            get
            {
                if (commandVerification == null)
                {
                    commandVerification = new ExchangeCommandVerification(testCandidate);
                }

                return commandVerification;
            }
        }

        private ExchangeExceptionVerification exceptionVerification = null;

        public ExchangeExceptionVerification ExceptionVerification
        {
            get
            {
                if (exceptionVerification == null)
                {
                    exceptionVerification = new ExchangeExceptionVerification(testCandidate);
                }

                return exceptionVerification;
            }
        }

        protected ExchangeVerificationBase(IUnauthenticatedExchange exchange)
        {
            testCandidate = exchange as Exchange;
            testCandidateType = exchange.GetType();
        }

        protected ExchangeVerificationBase(Type exchangeType)
        {
            GetAPIKeys(exchangeType);

            if (exchangeType == typeof(BitStamp))
                testCandidate = new BitStamp(ApiKey, Secret, ClientId);
            else if (exchangeType == typeof(Bitfinex))
                testCandidate = new Bitfinex(ApiKey, Secret);

            testCandidateType = exchangeType;
        }

        protected ExchangeVerificationBase(Exchange candidate)
        {
            ApiKey = candidate.Configuration.ApiKey;
            Secret = candidate.Configuration.SecretKey;
            ClientId = candidate.Configuration.ClientId;

            testCandidate = candidate;
            testCandidateType = candidate.GetType();
        }

        private void GetAPIKeys(Type exchangeType)
        {
            XElement keyFile = XElement.Load(@"C:\_Work\BEx\TestingKeys.xml");

            XElement exchangeElement = null;

            switch (exchangeType.ToString())
            {
                case ("BEx.BitStamp"):
                    exchangeElement = keyFile.Element("BitStamp");
                    break;

                case ("BEx.Bitfinex"):
                    exchangeElement = keyFile.Element("BitFinex");
                    break;

                case ("BEx.BTCe"):
                    exchangeElement = keyFile.Element("BTCe");
                    break;
            }

            ApiKey = exchangeElement.Element("Key").Value;
            Secret = exchangeElement.Element("Secret").Value;

            if (exchangeElement.Element("ClientID") != null)
                ClientId = exchangeElement.Element("ClientID").Value;
        }

        protected ExchangeVerificationBase()
        {
        }

        protected void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine("{0}: {1}", testCandidateType.ToString(), message);
        }
    }
}