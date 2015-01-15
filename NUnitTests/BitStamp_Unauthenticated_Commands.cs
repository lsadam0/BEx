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
    [Category("BitStamp.Unauthenticated")]
    public class BitStamp_Unauthenticated_Commands : VerifyExchangeBase
    {
        public BitStamp_Unauthenticated_Commands()
            : base(typeof(BEx.BitStamp))
        {
            toTest = new BitStamp(base.APIKey, base.Secret, base.ClientID);
        }

        [Test]
        public void BitStamp_GetTick_BTCUSD()
        {
            Tick t = toTest.GetTick();

            VerifyTick(t, Currency.BTC, Currency.USD);
        }

        [Test]
        public void BitStamp_GetOrderBook_BTCUSD()
        {

            OrderBook o = toTest.GetOrderBook();

            VerifyOrderBook(o);
        }

        [Test]
        public void BitStamp_GetTransactions_BTCUSD()
        {

            Transactions trans = toTest.GetTransactions();

            Assert.IsNotNull(trans);
        }

    }
}
