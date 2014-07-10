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
    [Category("BTCe")]
    public class BTCe_Commands : VerifyExchangeBase
    {

        BTCe be = new BTCe();

        #region Ticks
        [Test]
        public void BTCe_GetTick()
        {

            Tick t = be.GetTick();

            VerifyTick(t, Currency.BTC, Currency.USD);                
        }

        [Test]
        public void BTCe_GetTick_BTCRUR()
        {
            Tick t= be.GetTick(Currency.BTC, Currency.RUR);

            VerifyTick(t, Currency.BTC, Currency.RUR);
        }

        [Test]
        public void BTCe_GetTick_BTCEUR()
        {
            Tick t = be.GetTick(Currency.BTC, Currency.EUR);

            VerifyTick(t, Currency.BTC, Currency.EUR);
        }

        [Test]
        public void BTCe_GetTick_BTCCNH()
        {
            Tick t = be.GetTick(Currency.BTC, Currency.CNH);

            VerifyTick(t, Currency.BTC, Currency.CNH);
        }

        [Test]
        public void BTCe_GetTick_BTCGBP()
        {
            Tick t = be.GetTick(Currency.BTC, Currency.GBP);

            VerifyTick(t, Currency.BTC, Currency.GBP);
        }

        [Test]
        public void BTCe_GetTick_LTCBTC()
        {
            Tick t = be.GetTick(Currency.LTC, Currency.BTC);

            VerifyTick(t, Currency.LTC, Currency.BTC);
        }

        [Test]
        public void BTCe_GetTick_LTCUSD()
        {
            Tick t = be.GetTick(Currency.LTC, Currency.USD);

            VerifyTick(t, Currency.LTC, Currency.USD);
        }

        [Test]
        public void BTCe_GetTick_LTCRUR()
        {
            Tick t = be.GetTick(Currency.LTC, Currency.RUR);

            VerifyTick(t, Currency.LTC, Currency.RUR);
        }

        [Test]
        public void BTCe_GetTick_LTCEUR()
        {
            Tick t = be.GetTick(Currency.LTC, Currency.EUR);

            VerifyTick(t, Currency.LTC, Currency.EUR);
        }

        [Test]
        public void BTCe_GetTick_LTCCNH()
        {
            Tick t = be.GetTick(Currency.LTC, Currency.CNH);

            VerifyTick(t, Currency.LTC, Currency.CNH);
        }

        [Test]
        public void BTCe_GetTick_LTCGBP()
        {
            Tick t = be.GetTick(Currency.LTC, Currency.GBP);

            VerifyTick(t, Currency.LTC, Currency.GBP);
        }

        [Test]
        public void BTCe_GetTick_NMCBTC()
        {
            Tick t = be.GetTick(Currency.NMC, Currency.BTC);

            VerifyTick(t, Currency.NMC, Currency.BTC);
        }

        [Test]
        public void BTCe_GetTick_NMCUSD()
        {
            Currency baseC = Currency.NMC;
            Currency counterC = Currency.USD;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BTCe_GetTick_NVCBTC()
        {
            Currency baseC = Currency.NVC;
            Currency counterC = Currency.BTC;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BTCe_GetTick_NVCUSD()
        {
            Currency baseC = Currency.NVC;
            Currency counterC = Currency.USD;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BTCe_GetTick_USDRUR()
        {
            Currency baseC = Currency.USD;
            Currency counterC = Currency.RUR;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BTCe_GetTick_EURUSD()
        {
            Currency baseC = Currency.EUR;
            Currency counterC = Currency.USD;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BTCe_GetTick_EURRUR()
        {
            Currency baseC = Currency.EUR;
            Currency counterC = Currency.RUR;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BTCe_GetTick_USDCNH()
        {
            Currency baseC = Currency.USD;
            Currency counterC = Currency.CNH;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BTCe_GetTick_GBPUSD()
        {
            Currency baseC = Currency.GBP;
            Currency counterC = Currency.USD;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BTCe_GetTick_TRCBTC()
        {
            Currency baseC = Currency.TRC;
            Currency counterC = Currency.BTC;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BTCe_GetTick_PPCBTC()
        {
            Currency baseC = Currency.PPC;
            Currency counterC = Currency.BTC;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BTCe_GetTick_PPCUSD()
        {
            Currency baseC = Currency.PPC;
            Currency counterC = Currency.USD;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BTCe_GetTick_FTCBTC()
        {
            Currency baseC = Currency.FTC;
            Currency counterC = Currency.BTC;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BTCe_GetTick_XPMBTC()
        {
            Currency baseC = Currency.XPM;
            Currency counterC = Currency.BTC;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        #endregion

        
        #region OrderBook

        [Test]
        public void BTCe_GetOrderBook_BTCUSD()
        {
            Currency b = Currency.BTC;
            Currency c = Currency.USD;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_LTCBTC()
        {
            Currency b = Currency.LTC;
            Currency c = Currency.BTC;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_BTCCNH()
        {
            Currency b = Currency.BTC;
            Currency c = Currency.CNH;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_BTCEUR()
        {
            Currency b = Currency.BTC;
            Currency c = Currency.EUR;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_BTCGBP()
        {
            Currency b = Currency.BTC;
            Currency c = Currency.GBP;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_BTCRUR()
        {
            Currency b = Currency.BTC;
            Currency c = Currency.RUR;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_EURRUR()
        {
            Currency b = Currency.EUR;
            Currency c = Currency.RUR;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_EURUSD()
        {
            Currency b = Currency.EUR;
            Currency c = Currency.USD;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_FTCBTC()
        {
            Currency b = Currency.FTC;
            Currency c = Currency.BTC;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_GBPUSD()
        {
            Currency b = Currency.GBP;
            Currency c = Currency.USD;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_LTCCNH()
        {
            Currency b = Currency.LTC;
            Currency c = Currency.CNH;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_LTCEUR()
        {
            Currency b = Currency.LTC;
            Currency c = Currency.EUR;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_LTCGBP()
        {
            Currency b = Currency.LTC;
            Currency c = Currency.GBP;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_LTCRUR()
        {
            Currency b = Currency.LTC;
            Currency c = Currency.RUR;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_LTCUSD()
        {
            Currency b = Currency.LTC;
            Currency c = Currency.USD;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_NMCBTC()
        {
            Currency b = Currency.NMC;
            Currency c = Currency.BTC;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_NMCUSD()
        {
            Currency b = Currency.NMC;
            Currency c = Currency.USD;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_NVCBTC()
        {
            Currency b = Currency.NVC;
            Currency c = Currency.BTC;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_NVCUSD()
        {
            Currency b = Currency.LTC;
            Currency c = Currency.BTC;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }


        [Test]
        public void BTCe_GetOrderBook_PPCBTC()
        {
            Currency b = Currency.PPC;
            Currency c = Currency.BTC;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_PPCUSD()
        {
            Currency b = Currency.PPC;
            Currency c = Currency.USD;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_TRCBTC()
        {
            Currency b = Currency.TRC;
            Currency c = Currency.BTC;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_USDCNH()
        {
            Currency b = Currency.USD;
            Currency c = Currency.CNH;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_USDRUR()
        {
            Currency b = Currency.USD;
            Currency c = Currency.RUR;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }

        [Test]
        public void BTCe_GetOrderBook_XPMBTC()
        {
            Currency b = Currency.XPM;
            Currency c = Currency.BTC;

            OrderBook o = be.GetOrderBook(b, c);

            VerifyOrderBook(o);
        }
        #endregion
        

        #region Transactions

        [Test]
        public void BTCe_GetTransactions_BTCUSD()
        {
            Currency b = Currency.BTC;
            Currency c = Currency.USD;

            List<Transaction> t = be.GetTransactions(b, c);

            VerifyTransactions(t);

        }

        #endregion
         
    }
}
