using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BEx.ExchangeEngine
{
    internal interface IExchangeErrorInterpreter
    {
        Type Interpret(BExError error);
    }

    public abstract class ExchangeErrorInterpreter : IExchangeErrorInterpreter
    {
        private readonly IList<ExceptionIdentifier> _associations;

        internal ExchangeErrorInterpreter(IList<ExceptionIdentifier> associations)
        {
            _associations = associations;
        }

        public Type Interpret(BExError error)
        {
            foreach (ExceptionIdentifier candidate in _associations)
            {
                if (candidate.Pattern.IsMatch(error.Message))
                    return candidate.ExceptionType;
            }

            return typeof(Exception);
        }
    }

    internal class ExceptionIdentifier
    {
        internal ExceptionIdentifier(Regex pattern, Type exceptionType)
        {
            Pattern = pattern;
            ExceptionType = exceptionType;
        }

        public Type ExceptionType
        {
            get;
            private set;
        }

        public Regex Pattern
        {
            get;
            private set;
        }
    }
}