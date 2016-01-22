using System;
using System.Collections.Generic;
using RestSharp;

namespace BEx.ExchangeEngine.Commands
{
    internal class OpenOrdersCommand : ExchangeCommand
    {
        public OpenOrdersCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
                base(
                httpMethod, relativeUri, isAuthenticated, intermediateType, typeof (OpenOrders), parameters)
        {
        }

        public OpenOrdersCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType) :
                base(httpMethod, relativeUri, isAuthenticated, intermediateType, typeof (OpenOrders))
        {
        }
    }
}