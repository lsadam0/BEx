using System;
using System.Collections.Generic;
using RestSharp;

namespace BEx.ExchangeEngine.API.Commands
{
    internal class DepositAddressCommand : ExchangeCommand
    {
        public DepositAddressCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
                base(
                httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(DepositAddress),
                parameters)
        {
        }

        public DepositAddressCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType) :
                base(httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(DepositAddress))
        {
        }
    }
}