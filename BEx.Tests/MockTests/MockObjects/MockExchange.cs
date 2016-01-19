using BEx.ExchangeEngine;

namespace BEx.UnitTests.MockTests.MockObjects
{
    public class MockExchange : Exchange
    {
        internal MockExchange(IRequestDispatcher dispatcher)
            : base(new MockExchangeConfiguration(), new MockExchangeCommandFactory(), null, new MockExchangeAuthenticator(new MockExchangeConfiguration()))
        {
            base.Commands = new MockExchangeCommandFactory();
            base.Commands.BuildCommands(new ExecutionEngine(this.Configuration.BaseUri, this.Authenticator, ExchangeType.Mock));
        }
    }
}