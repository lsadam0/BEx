using NUnit.Framework;

namespace BEx.UnitTests.MockTests.MockObjects
{
    [TestFixture]
    [Category("A.MockExchange.RequestFailures")]
    public class MockExchange_Request_Failures
    {
        private MockExchange _failureExchange;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            _failureExchange = new MockExchange(new MockFailedRequestDispatcher());
        }

        [Test]
        public void MockExchange_GetTick_Timeout()
        {
            _failureExchange.GetTick();
        }
    }
}