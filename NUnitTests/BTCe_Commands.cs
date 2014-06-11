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
        
        [Test]
        
        public void BTCe_GetTick()
        {

            Tick t = be.GetTick();

            VerifyTick(t, Currency.BTC, Currency.USD);                
        }

        [Test]
        public void BTCe_GetBTCRURTick()
        {
            Tick t= be.GetTick(Currency.BTC, Currency.RUR);

            VerifyTick(t, Currency.BTC, Currency.RUR);
        }

        [Test]
        public void BTCe_GetBTCEURTick()
        {
            Tick t = be.GetTick(Currency.BTC, Currency.EUR);

            VerifyTick(t, Currency.BTC, Currency.EUR);
        }

        [Test]
        public void BTCe_GetBTCCNHTick()
        {
            Tick t = be.GetTick(Currency.BTC, Currency.CNH);

            VerifyTick(t, Currency.BTC, Currency.CNH);
        }

        [Test]
        public void BTCe_GetBTCGBPTick()
        {
            Tick t = be.GetTick(Currency.BTC, Currency.GBP);

            VerifyTick(t, Currency.BTC, Currency.GBP);
        }

        [Test]
        public void BTCe_GetLTCBTCTick()
        {
            Tick t = be.GetTick(Currency.LTC, Currency.BTC);

            VerifyTick(t, Currency.LTC, Currency.BTC);
        }

        [Test]
        public void BTCe_GetLTCUSDTick()
        {
            Tick t = be.GetTick(Currency.LTC, Currency.USD);

            VerifyTick(t, Currency.LTC, Currency.USD);
        }


        [Test]
        public void BTCe_GetLTCRURTick()
        {
            Tick t = be.GetTick(Currency.LTC, Currency.RUR);

            VerifyTick(t, Currency.LTC, Currency.RUR);
        }


        [Test]
        public void BTCe_GetLTCEURTick()
        {
            Tick t = be.GetTick(Currency.LTC, Currency.EUR);

            VerifyTick(t, Currency.LTC, Currency.EUR);
        }



        [Test]
        public void BTCe_GetLTCCNHTick()
        {
            Tick t = be.GetTick(Currency.LTC, Currency.CNH);

            VerifyTick(t, Currency.LTC, Currency.CNH);
        }


        [Test]
        public void BTCe_GetLTCGBPTick()
        {
            Tick t = be.GetTick(Currency.LTC, Currency.GBP);

            VerifyTick(t, Currency.LTC, Currency.GBP);
        }



        [Test]
        public void BTCe_GetNMCBTCTick()
        {
            Tick t = be.GetTick(Currency.NMC, Currency.BTC);

            VerifyTick(t, Currency.NMC, Currency.BTC);
        }

        [Test]
        public void BTCe_GetNMCUSDTick()
        {
            Currency baseC = Currency.NMC;
            Currency counterC = Currency.USD;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }


        [Test]
        public void BTCe_GetNVCBTCTick()
        {
            Currency baseC = Currency.NVC;
            Currency counterC = Currency.BTC;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }


        [Test]
        public void BTCe_GetNVCUSDTick()
        {
            Currency baseC = Currency.NVC;
            Currency counterC = Currency.USD;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }


        [Test]
        public void BTCe_GetUSDRURTick()
        {
            Currency baseC = Currency.USD;
            Currency counterC = Currency.RUR;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }


        [Test]
        public void BTCe_GetEURUSDTick()
        {
            Currency baseC = Currency.EUR;
            Currency counterC = Currency.USD;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }


        [Test]
        public void BTCe_GetEURRURTick()
        {
            Currency baseC = Currency.EUR;
            Currency counterC = Currency.RUR;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }


        [Test]
        public void BTCe_GetUSDCNHTick()
        {
            Currency baseC = Currency.USD;
            Currency counterC = Currency.CNH;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }


        [Test]
        public void BTCe_GetGBPUSDTick()
        {
            Currency baseC = Currency.GBP;
            Currency counterC = Currency.USD;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }


        [Test]
        public void BTCe_GetTRCBTCTick()
        {
            Currency baseC = Currency.TRC;
            Currency counterC = Currency.BTC;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }


        [Test]
        public void BTCe_GetPPCBTCTick()
        {
            Currency baseC = Currency.PPC;
            Currency counterC = Currency.BTC;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }


        [Test]
        public void BTCe_GetPPCUSDTick()
        {
            Currency baseC = Currency.PPC;
            Currency counterC = Currency.USD;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }


        [Test]
        public void BTCe_GetFTCBTCTick()
        {
            Currency baseC = Currency.FTC;
            Currency counterC = Currency.BTC;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }


        [Test]
        public void BTCe_GetXPMBTCTick()
        {
            Currency baseC = Currency.XPM;
            Currency counterC = Currency.BTC;

            Tick t = be.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }
    }
}
