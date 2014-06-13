using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using BEx;

namespace NUnitTests
{
    [Category("BitFinex")]
    [TestFixture]
    public class Bitfinex_Commands : VerifyExchangeBase
    {
        Bitfinex bfx = new Bitfinex();

        [Test]
        public void BitFinex_GetBTCUSDTick()
        {
            Currency baseC = Currency.BTC;
            Currency counterC = Currency.USD;

            Tick t = bfx.GetTick();

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BitFinex_GetLTCUSDTick()
        {
            Currency baseC = Currency.LTC;
            Currency counterC = Currency.USD;

            Tick t = bfx.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BitFinex_GetDRKUSDTick()
        {
            Currency baseC = Currency.DRK;
            Currency counterC = Currency.USD;

            Tick t = bfx.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BitFinex_GetLTCBTCTick()
        {
            Currency baseC = Currency.LTC;
            Currency counterC = Currency.BTC;

            Tick t = bfx.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BitFinex_GetDRKBTCTick()
        {
            Currency baseC = Currency.DRK;
            Currency counterC = Currency.BTC;

            Tick t = bfx.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }


        [Test]
        public void BitFinex_GetOrderBook()
        {
            OrderBook o = bfx.GetOrderBook();

            VerifyOrderBook(o);

        }
    }
}
