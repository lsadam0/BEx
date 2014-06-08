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
        }
    }
}
