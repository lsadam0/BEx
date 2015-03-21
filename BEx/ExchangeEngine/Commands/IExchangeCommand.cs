using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace BEx.ExchangeEngine
{
    internal interface IExchangeCommand
    {
        Type ApiResultSubType
        {
            get;

        }

        ExecutionEngine Executor { get; }

        Method HttpMethod { get; }

        Type IntermediateType { get; }

        bool IsAuthenticated { get; }

        IReadOnlyDictionary<string, ExchangeParameter> Parameters { get; }

        Uri RelativeUri { get; }

        bool ReturnsCollection { get; }

        bool ReturnsValueType { get; }

        ApiResult Execute(IDictionary<StandardParameter, string> parameters);
    }
}
