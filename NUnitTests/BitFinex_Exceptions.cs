using BEx;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("Bitfinex.Exceptions")]
    public class Bitfinex_Exceptions : VerifyExchangeBase
    {
        public Bitfinex_Exceptions()
            : base(typeof(BEx.Bitfinex))
        {
            toTest = null;
        }

        #region Authorization

        [Test]
        public void Bitfinex_MissingAPIKey_ExchangeAuthorizationException()
        {


            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    Bitfinex bt = new Bitfinex("", base.Secret);
                });

        }

        [Test]
        public void Bitfinex_IncorrectAPIKey_ExchangeAuthorizationException()
        {
            using (Bitfinex bt = new Bitfinex(base.APIKey.Remove(base.APIKey.Length - 1), base.Secret))
            {
                Assert.Throws<ExchangeAuthorizationException>(
                    delegate
                    {
                        bt.CreateSellOrder(1200m, 99000.00m);
                    });
            }
        }

        [Test]
        public void Bitfinex_MissingSecretKey_ExchangeAuthorizationException()
        {
            Assert.Throws<ExchangeAuthorizationException>(
                delegate
                {
                    Bitfinex bt = new Bitfinex(base.APIKey, "");
                });

        }

        [Test]
        public void Bitfinex_IncorrectSecretKey_ExchangeAuthorizationException()
        {
            using (Bitfinex bt = new Bitfinex(base.APIKey, base.Secret.Remove(base.Secret.Length - 1)))
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
        public void Bitfinex_CreateSellOrder_InsufficientFundsException()
        {
            using (Bitfinex bt = new Bitfinex(base.APIKey, base.Secret))
            {
                Assert.Throws<InsufficientFundsException>(
                delegate
                {
                    bt.CreateSellOrder(1200m, 99000.00m);
                });
            }
        }

        [Test]
        public void Bitfinex_CreateBuyOrder_InsufficientFundsException()
        {
            using (Bitfinex bt = new Bitfinex(base.APIKey, base.Secret))
            {
                Assert.Throws<InsufficientFundsException>(
                    delegate { bt.CreateSellOrder(1200m, 1.00m); }
                    );
            }
        }
    }
}