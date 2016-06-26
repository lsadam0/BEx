using NUnit.Framework;

namespace BEx.Tests
{
    [TestFixture]
    public class Scratch
    {
        [Test]
        public void Socket()
        {
            var stamp = ExchangeFactory.GetAuthenticatedExchange(ExchangeType.BitStamp);

            var response = stamp.GetAccountBalance();

            /*
             * var run = true;
            while (run)
            {
                {
                }
            }*/
        }
    }
}