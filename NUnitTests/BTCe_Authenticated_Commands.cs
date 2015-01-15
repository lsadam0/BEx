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
    [Category("BTCe.Authenticated")]
    public class BTCe_Authenticated_Commands : VerifyExchangeBase
    {
        public BTCe_Authenticated_Commands()
            : base(typeof(BEx.BTCe))
        {
            toTest = new BTCe(base.APIKey, base.Secret);
        }

        #region AccountBalance

        [Test]
        public void BTCe_GetAccountBalance()
        {
            AccountBalances o = toTest.GetAccountBalance();

            VerifyAccountBalance(o);

        }

        #endregion

    }
}
