using BEx;
using NUnit.Framework;
using System;

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
            try
            {
                using (Bitfinex bt = new Bitfinex("", base.Secret))
                {
                    Order res = bt.CreateSellOrder(1200m, 99000.00m);
                }
                throw new AssertionException("Expected an exception, but execution was successful");
            }
            catch (ExchangeAuthorizationException aex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(aex.Message));
            }
            catch (Exception)
            {
                throw new AssertionException("Expected ExchangeAuthorizationException");
            }
        }

        [Test]
        public void Bitfinex_IncorrectAPIKey_ExchangeAuthorizationException()
        {
            try
            {
                using (Bitfinex bt = new Bitfinex(base.APIKey.Remove(base.APIKey.Length - 1), base.Secret))
                {
                    Order res = bt.CreateSellOrder(1200m, 99000.00m);
                }

                throw new AssertionException("Expected an exception, but execution was successful");
            }
            catch (ExchangeAuthorizationException aex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(aex.Message));
            }
            catch (Exception)
            {
                throw new AssertionException("Expected ExchangeAuthorizationException");
            }
        }

        [Test]
        public void Bitfinex_MissingSecretKey_ExchangeAuthorizationException()
        {
            try
            {
                using (Bitfinex bt = new Bitfinex(base.APIKey, ""))
                {
                    Order res = bt.CreateSellOrder(1200m, 99000.00m);
                }
                throw new AssertionException("Expected an exception, but execution was successful");
            }
            catch (ExchangeAuthorizationException aex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(aex.Message));
            }
            catch (Exception)
            {
                throw new AssertionException("Expected ExchangeAuthorizationException");
            }
        }

        [Test]
        public void Bitfinex_IncorrectSecretKey_ExchangeAuthorizationException()
        {
            try
            {
                using (Bitfinex bt = new Bitfinex(base.APIKey, base.Secret.Remove(base.Secret.Length - 1)))
                {
                    Order res = bt.CreateSellOrder(1200m, 99000.00m);
                }
                throw new AssertionException("Expected an exception, but execution was successful");
            }
            catch (ExchangeAuthorizationException aex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(aex.Message));
            }
            catch (Exception)
            {
                throw new AssertionException("Expected ExchangeAuthorizationException");
            }
        }

        #endregion Authorization

        [Test]
        public void Bitfinex_CreateSellOrder_InsufficientFundsException()
        {
            try
            {
                using (Bitfinex bt = new Bitfinex(base.APIKey, base.Secret))
                {
                    Order res = bt.CreateSellOrder(1200m, 99000.00m);
                }
                throw new AssertionException("Expected an exception, but execution was successful");
            }
            catch (InsufficientFundsException iex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(iex.Message));
            }
            catch (Exception)
            {
                throw new AssertionException("Expected OrderRejectedException");
            }
        }

        [Test]
        public void Bitfinex_CreateBuyOrder_InsufficientFundsException()
        {
            try
            {
                using (Bitfinex bt = new Bitfinex(base.APIKey, base.Secret))
                {
                    Order res = bt.CreateBuyOrder(1200m, 1.00m);
                }
                throw new AssertionException("Expected an exception, but execution was successful");
            }
            catch (InsufficientFundsException iex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(iex.Message));
            }
            catch (Exception)
            {
                throw new AssertionException("Expected OrderRejectedException");
            }
        }

        
    }
}