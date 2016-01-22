using System;
using System.Collections.Generic;
using RestSharp;

namespace BEx.ExchangeEngine.Commands
{
    internal class CancelOrderCommand : ExchangeCommand
    {
        public CancelOrderCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
                base(
                httpMethod, relativeUri, isAuthenticated, intermediateType, typeof (Confirmation), parameters)
        {
        }
    }
}