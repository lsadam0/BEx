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
    public class BTCe_Commands
    {

        [Test]
        public void BTCe_GetTick()
        {

            BTCe be = new BTCe();

            Tick t = be.GetTick();

            Assert.IsNotNull(t);

            Assert.IsTrue(t.Ask > 0);
            Assert.IsTrue(t.Bid > 0);
            Assert.IsTrue(t.High > 0);
            Assert.IsTrue(t.Last > 0);
            Assert.IsTrue(t.Low > 0);
            Assert.IsTrue(t.Volume > 0);
            Assert.IsTrue(t.VWAP > 0);
        }
    }
}
