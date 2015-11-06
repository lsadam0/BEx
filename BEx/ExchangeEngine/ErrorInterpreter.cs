using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BEx.ExchangeEngine
{
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
        public Regex Pattern
        {
            get;
            private set;
        }

        public Type ExceptionType
        {
            get;
            private set;
        }

        internal ExceptionIdentifier(Regex pattern, Type exceptionType)
        {
            Pattern = pattern;
            ExceptionType = exceptionType;
        }
    }

    internal interface IExchangeErrorInterpreter
    {
        Type Interpret(BExError error);
    }
}