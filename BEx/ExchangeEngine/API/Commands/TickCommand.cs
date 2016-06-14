using System;
using System.Collections.Generic;
using RestSharp;

namespace BEx.ExchangeEngine.API.Commands
{
    internal class TickCommand : ExchangeCommand
    {
        public TickCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
                base(httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(Tick), parameters)
        {
        }

        public TickCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType) :
                base(httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(Tick))
        {
        }
    }
}