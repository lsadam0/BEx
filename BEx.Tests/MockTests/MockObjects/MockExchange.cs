using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEx.UnitTests.MockTests.MockObjects;
using BEx.ExchangeEngine;

namespace BEx.UnitTests.MockTests.MockObjects
{
    public class MockExchange : Exchange
    {
        public MockExchange()
            : base(new MockExchangeConfiguration(), new MockExchangeCommandFactory())
        {
            Authenticator = new MockExchangeAuthenticator(base.Configuration);

            base.Commands = new MockExchangeCommandFactory();
            base.Commands.BuildCommands(new ExecutionEngine(this, new MockRequestDispatcher()));
        }
    }
}
