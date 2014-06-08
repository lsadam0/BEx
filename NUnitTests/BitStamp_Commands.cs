using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using BEx;

namespace NUnitTests
{
    [TestFixture]
    public class BitStamp_Commands
    {
        [Test]
        public void BitStamp_GetTick()
        {
            BitStamp bts = new BitStamp();

            Tick t = bts.GetTick();

            Assert.IsNotNull(t);

            Assert.IsTrue(t.Ask > 0);
            Assert.IsTrue(t.Bid > 0);
            Assert.IsTrue(t.High > 0);
            Assert.IsTrue(t.Last > 0);
            Assert.IsTrue(t.Low > 0);
            Assert.IsTrue(t.Volume > 0);
            Assert.IsTrue(t.VWAP > 0);
        }


        [Test]
        public void BitStamp_GetOrderBook()
        {
            BitStamp bts = new BitStamp();

            OrderBook o = bts.GetOrderBook();

            Assert.IsNotNull(o);

        }

        [Test]
        public void BitStamp_GetTransactions()
        {
            BitStamp bts = new BitStamp();

            List<Transaction> trans = bts.GetTransactions();

            Assert.IsNotNull(trans);
        }
    }
}
