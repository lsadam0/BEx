using System;
using System.Diagnostics;

namespace BEx.ExchangeEngine.Utilities
{
    internal static class Debug
    {
        public static void Log(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Trace.WriteLine(message);
                Trace.WriteLine(Environment.NewLine);
            }
        }
    }
}