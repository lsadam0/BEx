using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using BEx.ExchangeEngine;

namespace BEx.ExchangeEngine.Commands
{
    internal class DepositAddressCommand : ExchangeCommand
    {
        public DepositAddressCommand(ExecutionEngine executor,
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType,
            IList<ExchangeParameter> parameters) :
            base(executor, httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(DepositAddress), parameters)
        {
        }

        public DepositAddressCommand(ExecutionEngine executor,
            Method httpMethod,
            Uri relativeUri,
            bool isAuthenticated,
            Type intermediateType) :
            base(executor, httpMethod, relativeUri, isAuthenticated, intermediateType, typeof(DepositAddress))
        {
        }

    }
}
