using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BEx.Tests.CoinbaseTests
{
    [TestFixture]
    [Category("Coinbase.UnauthenticatedCommands")]
    public class CoinbaseUnauthenticatedCommands : ExchangeVerificationBase
    {

        public CoinbaseUnauthenticatedCommands()
            : base(new Coinbase())
        { }

        [Test]
        public void Coinbase_GetTick_BTCUSD_Success()
        {
            CommandVerification.VerifyTick(TestCandidate.DefaultPair);
        }
    }
}
