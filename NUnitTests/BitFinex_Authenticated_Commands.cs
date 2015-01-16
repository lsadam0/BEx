using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitFinex.Authenticated")]
    public class BitFinex_Authenticated_Commands : VerifyExchangeBase
    {
        public BitFinex_Authenticated_Commands()
            : base(typeof(BEx.Bitfinex))
        {
            toTest = new Bitfinex(base.APIKey, base.Secret);

        }

        #region Account Balance Tests

        [Test]
        public void BitFinex_GetAccountBalance()
        {
            AccountBalances a = toTest.GetAccountBalance();

            VerifyAccountBalance(a);

        }

        #endregion

        #region Deposit Address
        [Test]
        public void BitFinex_GetBTCDepositAddress()
        {
            DepositAddress res = toTest.GetDepositAddress(Currency.BTC);


            { }
        }

        [Test]
        public void BitFinex_GetLTCDepositAddress()
        {
            DepositAddress res = toTest.GetDepositAddress(Currency.LTC);


            { }
        }

        [Test]
        public void BitFinex_GetDRKDepositAddress()
        {
            DepositAddress res = toTest.GetDepositAddress(Currency.DRK);


            { }
        }

        #endregion

        [Test]
        public void BitFinex_GetUserTransactions()
        {

            UserTransactions t = toTest.GetUserTransactions(Currency.BTC, Currency.USD);

            VerifyUserTransactions(t);
        }

        #region Orders

        [Test]
        public void BitFinex_CreateSellOrder()
        {

            Order o = toTest.CreateSellOrder(Currency.BTC, Currency.USD, 0.01m, 456.00m);

            VerifySellOrder(o);

        }

        [Test]
        public void BitFinex_CreateBuyOrder()
        {
            Order o = toTest.CreateBuyOrder(Currency.BTC, Currency.USD, 0.01m, 456.00m);

            VerifyBuyOrder(o);
        }

        [Test]
        public void BitFinex_GetOpenOrders()
        {
            OpenOrders o = toTest.GetOpenOrders();

            VerifyOpenOrders(o);
        }
        #endregion

    }
}
