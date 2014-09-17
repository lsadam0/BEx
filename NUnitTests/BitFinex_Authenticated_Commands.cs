using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using BEx;

namespace NUnitTests
{
    [TestFixture]
    [Category("BitFinex.Authenticated")]
    public class BitFinex_Authenticated_Commands :VerifyExchangeBase
    {
        public BitFinex_Authenticated_Commands() : base(typeof(BEx.Bitfinex))
        {
            toTest = new Bitfinex();

            toTest.APIKey = base.APIKey;
            toTest.SecretKey = base.Secret;
        }

        #region Account Balance Tests

        [Test]
        public void BitFinex_GetAccountBalance()
        {
            AccountBalance a = toTest.GetAccountBalance();

            VerifyAccountBalance(a);

        }

        #endregion

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

        [Test]
        public void BitFinex_GetUserTransactions()
        {

            UserTransactions t = toTest.GetUserTransactions(Currency.BTC, Currency.USD);

            VerifyUserTransactions(t);
        }

    }
}
