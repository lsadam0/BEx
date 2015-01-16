using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    
    [TestFixture]
    [Category("BitFinex.Unauthenticated")]
    public class BitFinex_Unauthenticated_Commands : VerifyExchangeBase
    {

        public BitFinex_Unauthenticated_Commands() : base(typeof(BEx.Bitfinex))
        {
            toTest = new Bitfinex(base.APIKey, base.Secret);

        }

        [Test]
        public void BitFinex_GetTick_BTCUSD()
        {
            Currency baseC = Currency.BTC;
            Currency counterC = Currency.USD;

            Tick t = toTest.GetTick();

            
            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BitFinex_GetTick_LTCUSD()
        {
            Currency baseC = Currency.LTC;
            Currency counterC = Currency.USD;

            Tick t = toTest.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BitFinex_GetTick_DRKUSD()
        {
            Currency baseC = Currency.DRK;
            Currency counterC = Currency.USD;

            Tick t = toTest.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BitFinex_GetTick_LTCBTC()
        {
            Currency baseC = Currency.LTC;
            Currency counterC = Currency.BTC;

            Tick t = toTest.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }

        [Test]
        public void BitFinex_GetTick_DRKBTC()
        {
            Currency baseC = Currency.DRK;
            Currency counterC = Currency.BTC;

            Tick t = toTest.GetTick(baseC, counterC);

            VerifyTick(t, baseC, counterC);
        }
        
        #region OrderBook Tests
        [Test]
        public void BitFinex_GetOrderBook_BTCUSD()
        {
            OrderBook o = toTest.GetOrderBook();

            VerifyOrderBook(o);

        }

        [Test]
        public void BitFinex_GetOrderBook_DRKBTC()
        {
            OrderBook o = toTest.GetOrderBook(Currency.DRK, Currency.BTC);

            VerifyOrderBook(o);
        }

        [Test]
        public void BitFinex_GetOrderBook_DRKUSD()
        {
            OrderBook o = toTest.GetOrderBook(Currency.DRK, Currency.USD);

            VerifyOrderBook(o);
        }


        [Test]
        public void BitFinex_GetOrderBook_LTCBTC()
        {
            OrderBook o = toTest.GetOrderBook(Currency.LTC, Currency.BTC);

            VerifyOrderBook(o);
        }
        [Test]
        public void BitFinex_GetOrderBook_LTCUSD()
        {
            OrderBook o = toTest.GetOrderBook(Currency.LTC, Currency.USD);

            VerifyOrderBook(o);
        }

        #endregion
        
        #region Transaction Tests
        [Test]
        public void BitFinex_GetTransactions_BTCUSD()
        {
            Transactions t = toTest.GetTransactions();

            VerifyTransactions(t);

        }

        [Test]
        public void BitFinex_GetTransactions_DRKBTC()
        {
            Transactions t = toTest.GetTransactions(Currency.DRK, Currency.BTC);

            VerifyTransactions(t);
        }


        [Test]
        public void BitFinex_GetTransactions_DRKUSD()
        {
            Transactions t = toTest.GetTransactions(Currency.DRK, Currency.USD);

            VerifyTransactions(t);
        }

        [Test]
        public void BitFinex_GetTransactions_LTCBTC()
        {
            Transactions t = toTest.GetTransactions(Currency.LTC, Currency.BTC);

            VerifyTransactions(t);
        }

        [Test]
        public void BitFinex_GetTransactions_LTCUSD()
        {
            Transactions t = toTest.GetTransactions(Currency.LTC, Currency.USD);

            VerifyTransactions(t);
        }

        #endregion

        

    }
}
