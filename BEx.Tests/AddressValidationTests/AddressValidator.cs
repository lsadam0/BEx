using NUnit.Framework;

namespace BEx.Tests.AddressValidationTests
{
    [TestFixture]
    [Category("AddressValidation")]
    public class AddressValidator
    {
        [Test]
        public void BTC_Address_IsValid()
        {
            var address = "37mJivamm5jS7rzVUtzPY1cJ9AHsZ4kanD";

            var res = ExchangeEngine.Utilities.AddressValidator.IsValid(address);

            Assert.IsTrue(res);
        }

        [Test]
        public void BTC_BitfinexBadResponse_IsInvalid()
        {
            var badResponse = "Unknown method";

            var res = ExchangeEngine.Utilities.AddressValidator.IsValid(badResponse);

            Assert.IsFalse(res);
        }

        [Test]
        public void LTC_Address_IsValid()
        {
            var address = "LfdF3ZympqjTBBxW2NDqBwF3CVRNXPZ84N";

            Assert.IsTrue(ExchangeEngine.Utilities.AddressValidator.IsValid(address));
        }
    }
}