using BEx;
using NUnit.Framework;
using System;

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
            try
            {
                using (BitStamp bt = new BitStamp("", base.Secret, base.ClientID))
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
        public void BitStamp_IncorrectAPIKey_ExchangeAuthorizationException()
        {
            try
            {
                using (BitStamp bt = new BitStamp(base.APIKey.Remove(base.APIKey.Length - 1), base.Secret, base.ClientID))
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
        public void BitStamp_MissingSecretKey_ExchangeAuthorizationException()
        {
            try
            {
                using (BitStamp bt = new BitStamp(base.APIKey, "", base.ClientID))
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
        public void BitStamp_IncorrectSecretKey_ExchangeAuthorizationException()
        {
            try
            {
                using (BitStamp bt = new BitStamp(base.APIKey, base.Secret.Remove(base.Secret.Length - 1), base.ClientID))
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
        public void BitStamp_MissingClientID_ExchangeAuthorizationException()
        {
            try
            {
                using (BitStamp bt = new BitStamp(base.APIKey, base.Secret, ""))
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
        public void BitStamp_IncorrectClientID_ExchangeAuthorizationException()
        {
            try
            {
                using (BitStamp bt = new BitStamp(base.APIKey, base.Secret, base.ClientID.Remove(this.ClientID.Length - 1)))
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
        public void BitStamp_CreateSellOrder_InsufficientFundsException()
        {
            try
            {
                using (BitStamp bt = new BitStamp(base.APIKey, base.Secret, base.ClientID))
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
        public void BitStamp_CreateBuyOrder_InsufficientFundsException()
        {
            try
            {
                using (BitStamp bt = new BitStamp(base.APIKey, base.Secret, base.ClientID))
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