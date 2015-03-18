using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("Bitfinex.UnauthenticatedCommands")]
    public class BitFinex_Unauthenticated_Commands : ExchangeVerificationBase
    {
        public BitFinex_Unauthenticated_Commands()
            : base(typeof(BEx.Bitfinex))
        {
        }

        [Test]
        public void GetTick_BTCUSD_Success()
        {
            CommandVerification.VerifyTick(new CurrencyTradingPair() { BaseCurrency = Currency.Btc, CounterCurrency = Currency.Usd });
        }

        [Test]
        public void GetTick_LTCUSD_Success()
        {
            CommandVerification.VerifyTick(new CurrencyTradingPair() { BaseCurrency = Currency.Ltc, CounterCurrency = Currency.Usd });
        }

        [Test]
        public void GetTick_LTCBTC_Success()
        {
            CommandVerification.VerifyTick(new CurrencyTradingPair() { BaseCurrency = Currency.Ltc, CounterCurrency = Currency.Btc });
        }

        [Test]
        public void GetTick_DRKUSD_Success()
        {
            CommandVerification.VerifyTick(new CurrencyTradingPair() { BaseCurrency = Currency.Drk, CounterCurrency = Currency.Usd });
        }

        [Test]
        public void GetTick_DRKBTC_Success()
        {
            CommandVerification.VerifyTick(new CurrencyTradingPair() { BaseCurrency = Currency.Drk, CounterCurrency = Currency.Btc });
        }

        [Test]
        public void GetOrderBook_BTCUSD_Success()
        {
            CommandVerification.VerifyOrderBook(testCandidate.DefaultPair);
        }

        [Test]
        public void GetOrderBook_LTCUSD_Success()
        {
            CommandVerification.VerifyOrderBook(new CurrencyTradingPair() { BaseCurrency = Currency.Ltc, CounterCurrency = Currency.Usd });
        }

        [Test]
        public void GetOrderBook_DRKUSD_Success()
        {
            CommandVerification.VerifyOrderBook(new CurrencyTradingPair() { BaseCurrency = Currency.Drk, CounterCurrency = Currency.Usd });
        }

        [Test]
        public void GetOrderBook_DRKBTC_Success()
        {
            CommandVerification.VerifyOrderBook(new CurrencyTradingPair() { BaseCurrency = Currency.Drk, CounterCurrency = Currency.Btc });
        }

        [Test]
        public void GetOrderBook_LTCBTC_Success()
        {
            CommandVerification.VerifyOrderBook(new CurrencyTradingPair() { BaseCurrency = Currency.Ltc, CounterCurrency = Currency.Btc });
        }

        [Test]
        public void GetTransactions_BTCUSD_Success()
        {
            CommandVerification.VerifyTransactions(new CurrencyTradingPair() { BaseCurrency = Currency.Btc, CounterCurrency = Currency.Usd });
        }

        [Test]
        public void GetTransactions_DRKBTC_Success()
        {
            CommandVerification.VerifyTransactions(new CurrencyTradingPair() { BaseCurrency = Currency.Drk, CounterCurrency = Currency.Btc });
        }

        [Test]
        public void GetTransactions_DRKUSD_Success()
        {
            CommandVerification.VerifyTransactions(new CurrencyTradingPair() { BaseCurrency = Currency.Drk, CounterCurrency = Currency.Usd });
        }

        [Test]
        public void GetTransactions_LTCBTC_Success()
        {
            CommandVerification.VerifyTransactions(new CurrencyTradingPair() { BaseCurrency = Currency.Ltc, CounterCurrency = Currency.Btc });
        }

        [Test]
        public void GetTransactions_LTCUSD_Success()
        {
            CommandVerification.VerifyTransactions(new CurrencyTradingPair() { BaseCurrency = Currency.Ltc, CounterCurrency = Currency.Usd });
        }
    }
}