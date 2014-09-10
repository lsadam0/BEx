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
    [Category("BitStamp.Exceptions")]
    public class BitStamp_Exceptions : VerifyExchangeBase
    {
        public BitStamp_Exceptions()
            : base(typeof(BEx.BitStamp))
        {
            toTest = new BitStamp();

            toTest.APIKey = base.APIKey;
            toTest.SecretKey = base.Secret;
            toTest.ClientID = base.ClientID;
        }

        [Test]
        public void BitStamp_MissingAPIKey_ExchangeAuthorizationException()
        {
            try
            {
                using (BitStamp bt = new BitStamp())
                {
                    toTest.APIKey = "";
                    toTest.SecretKey = base.Secret;
                    toTest.ClientID = base.ClientID;
                }

                Order res = toTest.CreateSellOrder(1200m, 99000.00m);

                throw new AssertionException("Expected an exception, but execution was successful");

            }
            catch (ExchangeAuthorizationException aex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(aex.Message));
            }
            catch (Exception ex)
            {
                throw new AssertionException("Expected ExchangeAuthorizationException");
            }
        }


        [Test]
        public void BitStamp_IncorrectAPIKey_ExchangeAuthorizationException()
        {
            try
            {
                using (BitStamp bt = new BitStamp())
                {
                    toTest.APIKey = base.APIKey.Remove(base.APIKey.Length - 1);
                    toTest.SecretKey = base.Secret;
                    toTest.ClientID = base.ClientID;
                }

                Order res = toTest.CreateSellOrder(1200m, 99000.00m);

                throw new AssertionException("Expected an exception, but execution was successful");

            }
            catch (ExchangeAuthorizationException aex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(aex.Message));
            }
            catch (Exception ex)
            {
                throw new AssertionException("Expected ExchangeAuthorizationException");
            }
        }

        [Test]
        public void BitStamp_MissingSecretKey_ExchangeAuthorizationException()
        {
            try
            {
                using (BitStamp bt = new BitStamp())
                {
                    toTest.APIKey = base.APIKey;
                    toTest.SecretKey = "";
                    toTest.ClientID = base.ClientID;
                }

                Order res = toTest.CreateSellOrder(1200m, 99000.00m);

                throw new AssertionException("Expected an exception, but execution was successful");

            }
            catch (ExchangeAuthorizationException aex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(aex.Message));
            }
            catch (Exception ex)
            {
                throw new AssertionException("Expected ExchangeAuthorizationException");
            }
        }


        [Test]
        public void BitStamp_IncorrectSecretKey_ExchangeAuthorizationException()
        {
            try
            {
                using (BitStamp bt = new BitStamp())
                {
                    toTest.APIKey = base.APIKey;
                    toTest.SecretKey = base.Secret.Remove(base.Secret.Length - 1);
                    toTest.ClientID = base.ClientID;
                }

                Order res = toTest.CreateSellOrder(1200m, 99000.00m);

                throw new AssertionException("Expected an exception, but execution was successful");

            }
            catch (ExchangeAuthorizationException aex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(aex.Message));
            }
            catch (Exception ex)
            {
                throw new AssertionException("Expected ExchangeAuthorizationException");
            }
        }

        [Test]
        public void BitStamp_MissingClientID_ExchangeAuthorizationException()
        {
            try
            {
                using (BitStamp bt = new BitStamp())
                {
                    toTest.APIKey = base.APIKey;
                    toTest.SecretKey = base.Secret;
                    toTest.ClientID = "";
                }

                Order res = toTest.CreateSellOrder(1200m, 99000.00m);

                throw new AssertionException("Expected an exception, but execution was successful");

            }
            catch (ExchangeAuthorizationException aex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(aex.Message));
            }
            catch (Exception ex)
            {
                throw new AssertionException("Expected ExchangeAuthorizationException");
            }
        }


        [Test]
        public void BitStamp_IncorrectClientID_ExchangeAuthorizationException()
        {
            try
            {
                using (BitStamp bt = new BitStamp())
                {
                    toTest.APIKey = base.APIKey;
                    toTest.SecretKey = base.Secret;
                    toTest.ClientID = base.ClientID.Remove(base.ClientID.Length - 1);
                }

                Order res = toTest.CreateSellOrder(1200m, 99000.00m);

                throw new AssertionException("Expected an exception, but execution was successful");

            }
            catch (ExchangeAuthorizationException aex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(aex.Message));
            }
            catch (Exception ex)
            {
                throw new AssertionException("Expected ExchangeAuthorizationException");
            }
        }

        [Test]
        public void BitStamp_CreateSellOrder_InsufficientFundsException()
        {
            try
            {
                Order res = toTest.CreateSellOrder(1200m, 99000.00m);

                throw new AssertionException("Expected an exception, but execution was successful");
            }
            catch (InsufficientFundsException iex)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(iex.Message));
            }
            catch (Exception ex)
            {
                throw new AssertionException("Expected InsufficientFundsException");
            }

        }
    }
}
