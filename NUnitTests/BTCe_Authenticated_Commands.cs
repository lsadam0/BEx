using BEx;
using NUnit.Framework;


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
