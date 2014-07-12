using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using NUnit.Framework;

using BEx;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitStamp")]
    public class BitStamp_Commands : VerifyExchangeBase
    {

        BitStamp bts;

        public BitStamp_Commands() : base()
        {
            bts = new BitStamp();

            // Get Secret Key


        }
        [Test]
        public void BitStamp_GetTick_BTCUSD()
        {
            Tick t = bts.GetTick();

            VerifyTick(t, Currency.BTC, Currency.USD);
        }

        
        [Test]
        public void BitStamp_GetOrderBook_BTCUSD()
        {

            OrderBook o = bts.GetOrderBook();

            VerifyOrderBook(o);
        }
        
        [Test]
        public void BitStamp_GetTransactions_BTCUSD()
        {

            List<Transaction> trans = bts.GetTransactions();
            
            Assert.IsNotNull(trans);
        }


    }
}
