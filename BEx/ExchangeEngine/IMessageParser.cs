using System;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace BEx.ExchangeEngine
{
    internal interface IMessageParser
    {
        void Parse(string message);
    }
}