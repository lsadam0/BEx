using BEx;
using System;
using System.Xml.Linq;

namespace NUnitTests
{
    public class ExchangeVerificationBase
    {
        protected Exchange testCandidate;
        protected Type testCandidateType;

        protected string APIKey
        {
            get;
            set;
        }

        protected string Secret
        {
            get;
            set;
        }

        protected string ClientID
        {
            get;
            set;
        }

        private static object testVelocityLock = new object();

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

        protected ExchangeVerificationBase(Type exchangeType)
        {
            GetAPIKeys(exchangeType);

            if (exchangeType == typeof(BitStamp))
                testCandidate = new BitStamp(APIKey, Secret, ClientID);
            else if (exchangeType == typeof(Bitfinex))
                testCandidate = new Bitfinex(APIKey, Secret);

            testCandidateType = exchangeType;
        }

        protected ExchangeVerificationBase(Exchange candidate)
        {
            APIKey = candidate.APIKey;// apiKey;
            Secret = candidate.SecretKey;//secret;
            ClientID = candidate.ClientID;

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

            APIKey = exchangeElement.Element("Key").Value;
            Secret = exchangeElement.Element("Secret").Value;

            if (exchangeElement.Element("ClientID") != null)
                ClientID = exchangeElement.Element("ClientID").Value;
        }

        protected ExchangeVerificationBase()
        {
        }

        protected void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine("{0}: {1}", testCandidateType.ToString(), message);
        }

        /// <summary>
        /// Exchanges ban API access for those that make excessive requests,
        /// in order to avoid the banhammer let's slow down the pace of testing
        /// so that at most we make one request every 2 seconds.
        /// </summary>
        protected void ThrottleTestVelocity()
        {
            /*
            lock (testVelocityLock)
            {
                new System.Threading.ManualResetEvent(false).WaitOne(2000);
            }*/
        }
    }
}