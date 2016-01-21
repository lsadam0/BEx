using System;
using System.Collections.Generic;
using RestSharp;

namespace BEx.ExchangeEngine.Commands
{
    internal class UserTransactionsCommand : ExchangeCommand<UserTransactions>
    {
        public UserTransactionsCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
                base(
                httpMethod, relativeUri, isAuthenticated, intermediateType, typeof (UserTransactions),
                parameters)
        {
        }

        public UserTransactionsCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType) :
                base(httpMethod, relativeUri, isAuthenticated, intermediateType, typeof (UserTransactions))
        {
        }
    }
}