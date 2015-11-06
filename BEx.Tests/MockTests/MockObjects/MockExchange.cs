using BEx.ExchangeEngine;

namespace BEx.UnitTests.MockTests.MockObjects
{
    public class MockExchange : Exchange
    {
        internal MockExchange(IRequestDispatcher dispatcher)
            : base(new MockExchangeConfiguration(), new MockExchangeCommandFactory(), null)
        {
            Authenticator = new MockExchangeAuthenticator(base.Configuration);

            base.Commands = new MockExchangeCommandFactory();
            base.Commands.BuildCommands(new ExecutionEngine(this, dispatcher));
        }
    }
}