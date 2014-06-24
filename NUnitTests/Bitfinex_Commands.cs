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
        public void BitFinex_GetTick_BTCUSD()
        {
            Currency baseC = Currency.BTC;
            Currency counterC = Currency.USD;

            Tick t = bfx.GetTick();

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BitFinex_GetTick_LTCUSD()
        {
            Currency baseC = Currency.LTC;
            Currency counterC = Currency.USD;

            Tick t = bfx.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BitFinex_GetTick_DRKUSD()
        {
            Currency baseC = Currency.DRK;
            Currency counterC = Currency.USD;

            Tick t = bfx.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BitFinex_GetTick_LTCBTC()
        {
            Currency baseC = Currency.LTC;
            Currency counterC = Currency.BTC;

            Tick t = bfx.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BitFinex_GetTick_DRKBTC()
        {
            Currency baseC = Currency.DRK;
            Currency counterC = Currency.BTC;

            Tick t = bfx.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }


        [Test]
        public void BitFinex_GetOrderBook_BTCUSD()
        {
            OrderBook o = bfx.GetOrderBook();

            VerifyOrderBook(o);

        }

        [Test]
        public void BitFinex_GetOrderBook_DRKBTC()
        {
            OrderBook o = bfx.GetOrderBook(Currency.DRK, Currency.BTC);

            VerifyOrderBook(o);
        }

        [Test]
        public void BitFinex_GetOrderBook_DRKUSD()
        {
            OrderBook o = bfx.GetOrderBook(Currency.DRK, Currency.USD);

            VerifyOrderBook(o);
        }


        [Test]
        public void BitFinex_GetOrderBook_LTCBTC()
        {
            OrderBook o = bfx.GetOrderBook(Currency.LTC, Currency.BTC);

            VerifyOrderBook(o);
        }
        [Test]
        public void BitFinex_GetOrderBook_LTCUSD()
        {
            OrderBook o = bfx.GetOrderBook(Currency.LTC, Currency.USD);

            VerifyOrderBook(o);
        }

        [Test]
        public void BitFinex_GetTransactions_BTCUSD()
        {
            List<Transaction> t = bfx.GetTransactions();

            VerifyTransactions(t);

        }

        [Test]
        public void BitFinex_GetTransactions_DRKBTC()
        {
            List<Transaction> t = bfx.GetTransactions(Currency.DRK, Currency.BTC);

            VerifyTransactions(t);
        }


        [Test]
        public void BitFinex_GetTransactions_DRKUSD()
        {
            List<Transaction> t = bfx.GetTransactions(Currency.DRK, Currency.USD);

            VerifyTransactions(t);
        }

        [Test]
        public void BitFinex_GetTransactions_LTCBTC()
        {
            List<Transaction> t = bfx.GetTransactions(Currency.LTC, Currency.BTC);

            VerifyTransactions(t);
        }

        [Test]
        public void BitFinex_GetTransactions_LTCUSD()
        {
            List<Transaction> t = bfx.GetTransactions(Currency.LTC, Currency.USD);

            VerifyTransactions(t);
        }


        [Test]
        public void BitFinex_GetTransactions_SinceDate_BTCUSD()
        {
            DateTime cutoff = DateTime.Now.AddMinutes(-30);
            List<Transaction> t = bfx.GetTransactions(cutoff, Currency.BTC, Currency.USD);

            VerifyTransactions(t);

        }
    }
}
