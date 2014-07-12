using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using BEx;

namespace NUnitTests
{
    

    public class VerifyExchangeBase
    {

        
        
        public static object testVelocityLock = new object();

        

        /// <summary>
        /// Exchanges ban API access for those that make excessive requests, 
        /// in order to avoid the banhammer let's slow down the pace of testing
        /// so that at most we make one request every 2 seconds.
        /// </summary>
        protected void ThrottleTestVelocity()
        {
            lock (testVelocityLock)
            {
                new System.Threading.ManualResetEvent(false).WaitOne(3000);
            }
        }

        protected void VerifyTick(Tick toVerify, Currency baseC, Currency counterC)
        {
            ThrottleTestVelocity();

            Assert.IsNotNull(toVerify);

            Assert.IsTrue(toVerify.BaseCurrency == baseC);
            Assert.IsTrue(toVerify.CounterCurrency == counterC);
            Assert.IsTrue(toVerify.Ask > 0);
            Assert.IsTrue(toVerify.Bid > 0);
            Assert.IsTrue(toVerify.High > 0);
            Assert.IsTrue(toVerify.Last > 0);
            Assert.IsTrue(toVerify.Low > 0);
            Assert.IsTrue(toVerify.Volume > 0);
            //Assert.IsTrue(toVerify.VWAP > 0);
        }

        protected void VerifyOrderBook(OrderBook toVerify)
        {
            ThrottleTestVelocity();

            Assert.IsNotNull(toVerify);

            Assert.IsTrue(toVerify.BidsByPrice.Keys.Count > 0);
            Assert.IsTrue(toVerify.AsksByPrice.Keys.Count > 0);
        }


        protected void VerifyTransactions(List<Transaction> toVerify)
        {
            ThrottleTestVelocity();

            Assert.IsNotNull(toVerify);
            Assert.IsTrue(toVerify.Count > 0);
        }


    }
}
