using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace BEx.ExchangeEngine.Commands
{
    internal class TickCommand : ExchangeCommand
    {
        public TickCommand(ExecutionEngine executor,
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
            base(executor, httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(Tick), parameters)
        {
        }

        public TickCommand(ExecutionEngine executor,
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType) :
            base(executor, httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(Tick))
        {
        }

    }
}
