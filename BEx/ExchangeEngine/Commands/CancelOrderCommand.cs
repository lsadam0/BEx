using RestSharp;
using System;
using System.Collections.Generic;

namespace BEx.ExchangeEngine.Commands
{
    internal class CancelOrderCommand : ExchangeCommand<Confirmation>
    {
        public CancelOrderCommand(ExecutionEngine executor,
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
            base(executor, httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(Confirmation), parameters)
        {
        }
    }
}