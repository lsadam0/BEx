﻿using System;
using System.Collections.Generic;
using RestSharp;

namespace BEx.ExchangeEngine.API.Commands
{
    internal interface IExchangeCommand
    {
        Type ApiResultSubType { get; }

        Method HttpMethod { get; }

        Type IntermediateType { get; }

        bool IsAuthenticated { get; }

        IReadOnlyDictionary<string, ExchangeParameter> Parameters { get; }

        Uri RelativeUri { get; }

        bool ReturnsCollection { get; }

        bool ReturnsValueType { get; }
    }
}