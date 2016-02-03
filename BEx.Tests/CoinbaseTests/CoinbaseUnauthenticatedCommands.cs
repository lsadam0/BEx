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


        [Test]
        public void Coinbase_GetDayRange_BTCUSD_Success()
        {
            CommandVerification.VerifyDayRange(TestCandidate.DefaultPair);
        }


        [Test]
        public void Coinbase_GetOrderBook_BTCUSD_Success()
        {
            CommandVerification.VerifyOrderBook(TestCandidate.DefaultPair);
        }


        [Test]
        public void Coinbase_GetTransactions_BTCUSD_Success()
        {
            CommandVerification.VerifyTransactions(TestCandidate.DefaultPair);
        }
    }
}
