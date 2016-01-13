using RestSharp;
using System;
using System.Collections.Generic;

namespace BEx.ExchangeEngine.Commands
{
    internal class UserTransactionsCommand : ExchangeCommand<UserTransactions>
    {
        public UserTransactionsCommand(ExecutionEngine executor,
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
            base(executor, httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(UserTransactions), parameters)
        {
        }

        public UserTransactionsCommand(ExecutionEngine executor,
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType) :
            base(executor, httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(UserTransactions))
        {
        }
    }
}