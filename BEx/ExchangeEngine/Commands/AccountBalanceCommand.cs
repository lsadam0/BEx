using RestSharp;
using System;
using System.Collections.Generic;

namespace BEx.ExchangeEngine.Commands
{
    internal class AccountBalanceCommand : ExchangeCommand<AccountBalance>
    {
        public AccountBalanceCommand(
            ExecutionEngine executor,
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
            base(executor, httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(AccountBalance), parameters)
        {
        }

        public AccountBalanceCommand(
            ExecutionEngine executor,
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType) :
            base(executor, httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(AccountBalance))
        {
        }
    }
}