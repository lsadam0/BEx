using System;
using System.Collections.Generic;
using RestSharp;

namespace BEx.ExchangeEngine.Commands
{
    internal class DayRangeCommand : ExchangeCommand
    {
        public DayRangeCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
                base(httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(Tick), parameters)
        {
        }

        public DayRangeCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType) :
                base(httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(Tick))
        {
        }
    }
}