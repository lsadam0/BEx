using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using NUnit.Framework;

using BEx;

namespace NUnitTests
{
    [TestFixture]
    [Category("BTCe.Exceptions")]
    public class BTCe_Exceptions : VerifyExchangeBase
    {
        public BTCe_Exceptions()
            : base(typeof(BEx.BTCe))
        {
            toTest = null;

        }

        #region Authorization
        [Test]
        public void BTCe_MissingAPIKey_ExchangeAuthorizationException()
        {
            try
            {
                using (BTCe bt = new BTCe("", base.Secret))
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
        public void BTCe_IncorrectAPIKey_ExchangeAuthorizationException()
        {
            try
            {
                using (BTCe bt = new BTCe(base.APIKey.Remove(base.APIKey.Length - 1), base.Secret))
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
        public void BTCe_MissingSecretKey_ExchangeAuthorizationException()
        {
            try
            {
                using (BTCe bt = new BTCe(base.APIKey, ""))
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
        public void BTCe_IncorrectSecretKey_ExchangeAuthorizationException()
        {
            try
            {
                using (BTCe bt = new BTCe(base.APIKey, base.Secret.Remove(base.Secret.Length - 1)))
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



        #endregion

        [Test]
        public void BTCe_CreateSellOrder_InsufficientFundsException()
        {
            try
            {
                using (BTCe bt = new BTCe(base.APIKey, base.Secret))
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
        public void BTCe_CreateBuyOrder_InsufficientFundsException()
        {
            try
            {
                using (BTCe bt = new BTCe(base.APIKey, base.Secret))
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

        [Test]
        public void BTCe_Withdrawal_Exception()
        {
            // invalid amount
            // bad address
            try
            {
                using (BTCe bt = new BTCe(base.APIKey, base.Secret))
                {
                    object res = bt.Withdraw(Currency.BTC, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 45665456.01m);
                }
                { }
            }
            catch (WithdrawalRejectedException)
            {
                { }
            }
            catch (Exception)
            {
                { }

            }
        }
    }
}