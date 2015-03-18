using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("Bitfinex.AuthenticatedCommands")]
    public class BitFinex_Authenticated_Commands : ExchangeVerificationBase
    {
        public BitFinex_Authenticated_Commands()
            : base(typeof(BEx.Bitfinex))
        {
        }

        [Test]
        public void GetAccountBalance_Success()
        {
            CommandVerification.VerifyAccountBalance();
        }

        [Test]
        public void GetDepositAddress_BTC_Success()
        {
            CommandVerification.VerifyDepositAddress(Currency.BTC);
        }

        [Test]
        public void GetDepositAddress_LTC_Success()
        {
            CommandVerification.VerifyDepositAddress(Currency.LTC);
        }

        [Test]
        public void GetDepositAddress_DRK_Success()
        {
            CommandVerification.VerifyDepositAddress(Currency.DRK);
        }

        [Test]
        public void GetUserTransactions_BTCUSD_Success()
        {
            CommandVerification.VerifyUserTransactions(testCandidate.DefaultPair);
        }

        [Test]
        public void GetUserTransactions_DRKBTC_Success()
        {
            CommandVerification.VerifyUserTransactions(new CurrencyTradingPair() { BaseCurrency = Currency.DRK, CounterCurrency = Currency.BTC });
        }

        [Test]
        public void GetUserTransactions_DRKUSD_Success()
        {
            CommandVerification.VerifyUserTransactions(new CurrencyTradingPair() { BaseCurrency = Currency.DRK, CounterCurrency = Currency.USD });
        }

        [Test]
        public void GetUserTransactions_LTCBTC_Success()
        {
            CommandVerification.VerifyUserTransactions(new CurrencyTradingPair() { BaseCurrency = Currency.LTC, CounterCurrency = Currency.BTC });
        }

        [Test]
        public void GetUserTransactions_LTCUSD_Success()
        {
            CommandVerification.VerifyUserTransactions(new CurrencyTradingPair() { BaseCurrency = Currency.LTC, CounterCurrency = Currency.USD });
        }

        [Test]
        public void GetOpenOrders_Success()
        {
            CommandVerification.VerifyOpenOrders();
        }
    }
}