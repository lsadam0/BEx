// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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


        protected  ExchangeVerificationBase()
        { }

        protected ExchangeVerificationBase(IUnauthenticatedExchange exchange)
        {
            testCandidate = exchange as Exchange;
        }

      
        protected void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine("{0}: {1}", testCandidate.ExchangeSourceType, message);
        }
    }
}