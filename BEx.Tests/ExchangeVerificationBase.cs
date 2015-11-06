// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BEx.UnitTests
{
    public class ExchangeVerificationBase
    {
        protected Exchange TestCandidate
        {
            get;
            set;
        }

        protected ExchangeCommandVerification CommandVerification
        {
            get;
            set;
        }

        public ExchangeVerificationBase(Exchange testCanddiate)
        {
            TestCandidate = testCanddiate;
        }

        public ExchangeVerificationBase()
        {
        }

        protected void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine("{0}: {1}", TestCandidate.ExchangeSourceType, message);
        }
    }
}