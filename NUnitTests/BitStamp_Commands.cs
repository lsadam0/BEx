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
        public BitStamp_Commands() : base(typeof(BEx.BitStamp))
        {
            toTest = new BitStamp();

            toTest.APIKey = base.APIKey;
            toTest.SecretKey = base.Secret;
            toTest.ClientID = base.ClientID;
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

            List<Transaction> trans = toTest.GetTransactions();
            
            Assert.IsNotNull(trans);
        }


        [Test]
        public void BitStamp_GetAccountBalance()
        {
            object res = toTest.GetAccountBalance();

            VerifyAccountBalance(res);

        }

    }
}
