using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitStamp.Exceptions")]
    public class BitStamp_Exceptions : VerifyExchangeBase
    {
        public BitStamp_Exceptions()
            : base(typeof(BEx.BitStamp))
        {
            toTest = null;
        }

        #region Authorization

        [Test]
        public void BitStamp_MissingAPIKey_ExchangeAuthorizationException()
        {

            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    BitStamp bt = new BitStamp("", base.Secret, base.ClientID);
                });

        }

        [Test]
        public void BitStamp_IncorrectAPIKey_ExchangeAuthorizationException()
        {
            using (BitStamp bt = new BitStamp(base.APIKey.Remove(base.APIKey.Length - 1), base.Secret, base.ClientID))
            {
                Assert.Throws<ExchangeAuthorizationException>(
                    delegate
                    {
                        bt.CreateSellOrder(1200m, 99000.00m);
                    });
            }
        }

        [Test]
        public void BitStamp_MissingSecretKey_ExchangeAuthorizationException()
        {
            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    BitStamp bt = new BitStamp(base.APIKey, "", base.ClientID);
                });

        }

        [Test]
        public void BitStamp_IncorrectSecretKey_ExchangeAuthorizationException()
        {
            using (BitStamp bt = new BitStamp(base.APIKey, base.Secret.Remove(base.Secret.Length - 1), base.ClientID))
            {
                Assert.Throws<ExchangeAuthorizationException>(
                    delegate
                    {
                        bt.CreateSellOrder(1200m, 99000.00m);
                    });
            }
        }

        [Test]
        public void BitStamp_MissingClientID_ExchangeAuthorizationException()
        {

            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    BitStamp bt = new BitStamp(base.APIKey, base.Secret, "");
                });

        }

        [Test]
        public void BitStamp_IncorrectClientID_ExchangeAuthorizationException()
        {
            using (BitStamp bt = new BitStamp(base.APIKey, base.Secret, base.ClientID.Remove(this.ClientID.Length - 1)))
            {
                Assert.Throws<ExchangeAuthorizationException>(
                    delegate
                    {
                        bt.CreateSellOrder(1200m, 99000.00m);
                    });
            }
        }

        #endregion Authorization

        [Test]
        public void BitStamp_CreateSellOrder_InsufficientFundsException()
        {
            using (BitStamp bt = new BitStamp(base.APIKey, base.Secret, base.ClientID))
            {
                Assert.Throws<InsufficientFundsException>(
                    delegate
                    {
                        bt.CreateSellOrder(1200m, 99000.00m);
                    });
            }
        }

        [Test]
        public void BitStamp_CreateBuyOrder_InsufficientFundsException()
        {
            using (BitStamp bt = new BitStamp(base.APIKey, base.Secret, base.ClientID))
            {
                Assert.Throws<InsufficientFundsException>(
                    delegate
                    {
                        bt.CreateBuyOrder(1200m, 1.00m);
                    });
            }
        }
    }
}