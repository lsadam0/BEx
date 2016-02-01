// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace BEx.Tests
{
    public class ExchangeVerificationBase
    {
        public ExchangeVerificationBase(Exchange testCanddiate)
        {
            TestCandidate = testCanddiate;
            CommandVerification = new ExchangeCommandVerification(testCanddiate);
        }

        protected Exchange TestCandidate { get; }

        protected ExchangeCommandVerification CommandVerification { get; }

        protected void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine("{0}: {1}", TestCandidate.ExchangeSourceType, message);
        }

       
    }
}