using System;
using System.Collections.Generic;
using RestSharp;

namespace BEx.ExchangeEngine.API.Commands
{
    internal class OrderBookCommand : ExchangeCommand
    {
        public OrderBookCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
                base(
                httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(OrderBook), parameters)
        {
        }

        public OrderBookCommand(
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType) :
                base(httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(OrderBook))
        {
        }
    }
}