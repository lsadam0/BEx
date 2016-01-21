using System;
using System.Collections.Generic;
using RestSharp;

namespace BEx.ExchangeEngine.Commands
{
    internal class DepositAddressCommand : ExchangeCommand<DepositAddress>
    {
        public DepositAddressCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
                base(
                httpMethod, relativeUri, isAuthenticated, intermediateType, typeof (DepositAddress),
                parameters)
        {
        }

        public DepositAddressCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType) :
                base(httpMethod, relativeUri, isAuthenticated, intermediateType, typeof (DepositAddress))
        {
        }
    }
}