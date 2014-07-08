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
        public void BitStamp_GetTick_BTCUSD()
        {
            BitStamp bts = new BitStamp();

            Tick t = bts.GetTick();


            VerifyTick(t, Currency.BTC, Currency.USD);

            //Assert.IsTrue(t.VolumeWeightedAveragePrice > 0);
        }

        /*
        [Test]
        public void BitStamp_GetOrderBook_BTCUSD()
        {
            BitStamp bts = new BitStamp();

            OrderBook o = bts.GetOrderBook();

            VerifyOrderBook(o);

        }
        */
        [Test]
        public void BitStamp_GetTransactions_BTCUSD()
        {
            BitStamp bts = new BitStamp();

            List<Transaction> trans = bts.GetTransactions();

            
            Assert.IsNotNull(trans);
        }


    }
}
