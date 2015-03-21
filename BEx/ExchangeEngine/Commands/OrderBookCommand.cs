using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace BEx.ExchangeEngine.Commands
{
    internal class OrderBookCommand : ExchangeCommand
    {
        public OrderBookCommand(ExecutionEngine executor,
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
            base(executor, httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(OrderBook), parameters)
        {
        }

        public OrderBookCommand(ExecutionEngine executor,
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType) :
            base(executor, httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(OrderBook))
        {
        }

    }
}
