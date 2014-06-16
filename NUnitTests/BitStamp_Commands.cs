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
    [Category("BitStamp")]
    public class BitStamp_Commands : VerifyExchangeBase
    {
        [Test]
        public void BitStamp_GetTick()
        {
            BitStamp bts = new BitStamp();

            BitstampTick t = (BitstampTick)bts.GetTick();


            VerifyTick(t, Currency.BTC, Currency.USD);

            Assert.IsTrue(t.VolumeWeightedAveragePrice > 0);
        }


        [Test]
        public void BitStamp_GetOrderBook()
        {
            BitStamp bts = new BitStamp();

            OrderBook o = bts.GetOrderBook();

            VerifyOrderBook(o);

        }

        [Test]
        public void BitStamp_GetTransactions()
        {
            BitStamp bts = new BitStamp();

            List<Transaction> trans = bts.GetTransactions();

            Assert.IsNotNull(trans);
        }

        [Test]
        public void BitStamp_GetTransactions_Parameters()
        {
            BitStamp bts = new BitStamp();

            List<Transaction> trans = bts.GetTransactions("hour");

            Assert.IsNotNull(trans);
        }
    }
}
