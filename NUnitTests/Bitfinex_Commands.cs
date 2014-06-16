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
        public void BitFinex_GetOrderBookBTCUSD()
        {
            OrderBook o = bfx.GetOrderBook();

            VerifyOrderBook(o);

        }

        [Test]
        public void BitFinex_GetOrderBookDRKBTC()
        {
            OrderBook o = bfx.GetOrderBook(Currency.DRK, Currency.BTC);

            VerifyOrderBook(o);
        }

        [Test]
        public void BitFinex_GetOrderBookDRKUSD()
        {
            OrderBook o = bfx.GetOrderBook(Currency.DRK, Currency.USD);

            VerifyOrderBook(o);
        }


        [Test]
        public void BitFinex_GetOrderBookLTCBTC()
        {
            OrderBook o = bfx.GetOrderBook(Currency.LTC, Currency.BTC);

            VerifyOrderBook(o);
        }
        [Test]
        public void BitFinex_GetOrderBookLTCUSD()
        {
            OrderBook o = bfx.GetOrderBook(Currency.LTC, Currency.USD);

            VerifyOrderBook(o);
        }

        [Test]
        public void BitFinex_GetTransactionsBTCUSD()
        {
            List<Transaction> t = bfx.GetTransactions();

            VerifyTransactions(t);

        }

        [Test]
        public void BitFinex_GetTransactionsDRKBTC()
        {
            List<Transaction> t = bfx.GetTransactions(Currency.DRK, Currency.BTC);

            VerifyTransactions(t);
        }


        [Test]
        public void BitFinex_GetTransactionsDRKUSD()
        {
            List<Transaction> t = bfx.GetTransactions(Currency.DRK, Currency.USD);

            VerifyTransactions(t);
        }

        [Test]
        public void BitFinex_GetTransactionsLTCBTC()
        {
            List<Transaction> t = bfx.GetTransactions(Currency.LTC, Currency.BTC);

            VerifyTransactions(t);
        }

        [Test]
        public void BitFinex_GetTransactionsLTCUSD()
        {
            List<Transaction> t = bfx.GetTransactions(Currency.LTC, Currency.USD);

            VerifyTransactions(t);
        }


        
    }
}
