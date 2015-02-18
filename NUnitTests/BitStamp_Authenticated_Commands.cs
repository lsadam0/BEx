using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitStamp.Authenticated")]
    public class BitStamp_Authenticated_Commands : VerifyExchangeBase
    {
        public BitStamp_Authenticated_Commands()
            : base(typeof(BEx.BitStamp))
        {
            toTest = new BitStamp(base.APIKey, base.Secret, base.ClientID);
        }

        [Test]
        public void BitStamp_GetAccountBalance()
        {
            VerifyAccountBalance();
        }

        [Test]
        public void BitStamp_CreateBuyOrder()
        {
            VerifyBuyOrder();
        }

        [Test]
        public void BitStamp_CreateSellOrder()
        {
            VerifySellOrder();
        }

        [Test]
        public void BitStamp_GetOpenOrders()
        {
            VerifyOpenOrders();
        }

        [Test]
        public void BitStamp_GetUserTransactions()
        {
            VerifyUserTransactions();
        }

        [Test]
        public void BitStamp_GetDepositAddress()
        {
            VerifyDepositAddress();
        }
    }
}