using System;
using System.Collections.Generic;
using RestSharp;

namespace BEx.ExchangeEngine.Commands
{
    internal class LimitOrderCommand : ExchangeCommand
    {
        public LimitOrderCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
                base(httpMethod, relativeUri, isAuthenticated, intermediateType, typeof (Order), parameters)
        {
        }
    }
}