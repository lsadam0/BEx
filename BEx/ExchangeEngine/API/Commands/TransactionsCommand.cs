using System;
using System.Collections.Generic;
using RestSharp;

namespace BEx.ExchangeEngine.API.Commands
{
    internal class TransactionsCommand : ExchangeCommand
    {
        public TransactionsCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
                base(
                httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(Transactions), parameters)
        {
        }
    }
}