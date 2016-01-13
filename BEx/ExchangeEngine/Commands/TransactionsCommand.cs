using RestSharp;
using System;
using System.Collections.Generic;

namespace BEx.ExchangeEngine.Commands
{
    internal class TransactionsCommand : ExchangeCommand<Transactions>
    {
        public TransactionsCommand(ExecutionEngine executor,
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
            base(executor, httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(Transactions), parameters)
        {
        }
    }
}