using System;
using System.Collections.Generic;
using RestSharp;

namespace BEx.ExchangeEngine.API.Commands
{
    internal class AccountBalanceCommand : ExchangeCommand
    {
        public AccountBalanceCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
                base(
                httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(AccountBalance),
                parameters)
        {
        }

        public AccountBalanceCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType) :
                base(httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(AccountBalance))
        {
        }
    }
}